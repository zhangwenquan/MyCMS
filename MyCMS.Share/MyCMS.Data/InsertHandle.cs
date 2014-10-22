using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public class InsertHandle : OperateHandle
    {

        private string fieldValues;

        public string FieldValues
        {
            get { return fieldValues; }
        }
        private int returnCode;

        public int ReturnCode
        {
            get { return returnCode; }
        }
        private object returnObj;

        public object ReturnObj
        {
            get { return returnObj; }
        }

        void BuildFieldsValue()
        {
            StringBuilder s = new StringBuilder();
            foreach (Property p in EntityObject.PropertyDict.Values)
            {
                if (!ListFieldDict.ContainsKey(p.Name))
                    continue;
                s.Append(AddParameter(p, p.Info.GetValue(ExecuteObject)));
                s.Append(",");
            }
            s.Length -= 1;
            fieldValues = s.ToString();
        }

        public override void Build(bool forContent = false)
        {
            BuildFields(false);
            BuildFieldsValue();
            Sql.SqlClause = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", Connection.Driver.FormatTable(EntityObject.TableName), Fields, FieldValues);
        }

        public void Execute()
        {
            returnObj = null;
            Build();
            returnCode = Connection.Update(Sql);

            if (EntityObject.IsIdentity)
            {
                SqlStatement s = new SqlStatement("SELECT @@identity");
                returnObj = Connection.QueryScalar(s);
                EntityObject.PropertyDict[EntityObject.IdentityName].Info.SetValue(ExecuteObject, returnObj);
            }
        }
    }

    public class SelectHandle : OperateHandle
    {

        public override void Build(bool forContent = false)
        {
            BuildFields(true);
            BuildCondition();
            if (!string.IsNullOrEmpty(Condition))
                Sql.SqlClause = string.Format("SELECT {0} FROM {1} WHERE {2}", Fields, Connection.Driver.FormatTable(EntityObject.TableName), Condition);
            else
                Sql.SqlClause = string.Format("SELECT {0} FROM {1}", Fields, Connection.Driver.FormatTable(EntityObject.TableName));
        }

        public void Execute()
        {
            Build();
            DataTable table = Connection.Query(Sql);
            if (table.Rows.Count > 0)
                BindObject(ExecuteObject, table.Rows[0]);
            else
                throw new Exception("No Data");
        }
    }

    public class UpdateHandle : OperateHandle
    {

        private int returnCode;

        public int ReturnCode
        {
            get { return returnCode; }
        }
        private string fieldsSet;

        public string FieldsSet
        {
            get { return fieldsSet; }
        }

        void BuildFieldsSet()
        {
            StringBuilder s = new StringBuilder();
            foreach (Property p in EntityObject.PropertyDict.Values)
            {
                if (p.Readonly || ListFieldDict.Count > 0 && !ListFieldDict.ContainsKey(p.Field))
                    continue;
                s.AppendFormat("{0} = {1},", Connection.Driver.FormatField(Adorns.None, p.Field), AddParameter(p, p.Info.GetValue(ExecuteObject)));
            }
            s.Length -= 1;
            fieldsSet = s.ToString();
        }

        public override void Build(bool forContent = false)
        {
            BuildFieldsSet();
            BuildCondition();
            if (!string.IsNullOrEmpty(Condition))
                Sql.SqlClause = string.Format("UPDATE {0} SET {1} WHERE {2}", Connection.Driver.FormatTable(EntityObject.TableName), FieldsSet, Condition);
            else
                Sql.SqlClause = string.Format("UPDATE {0} SET {1}", Connection.Driver.FormatTable(EntityObject.TableName), FieldsSet);
        }

        public void Execute()
        {
            Build();
            returnCode = Connection.Update(Sql);
        }
    }

    public class DeleteHandle : BaseHandle
    {
        private int returnCode;

        public int ReturnCode
        {
            get { return returnCode; }
        }
        
        public override void Build(bool forContent = false)
        {
            BuildCondition();
            if (!string.IsNullOrEmpty(Condition))
                Sql.SqlClause = string.Format("DELETE FROM {0} WHERE {1}", Connection.Driver.FormatTable(EntityObject.TableName), Condition);
            else
                Sql.SqlClause = string.Format("DELETE FROM {0}", Connection.Driver.FormatTable(EntityObject.TableName), Condition);
        }

        public void Execute()
        {
            Build();
            returnCode = Connection.Update(Sql);
        }
    }

    public class CountHandle : OperateHandle
    {
        private int returnCode;

        public int ReturnCode
        {
            get { return returnCode; }
            set { returnCode = value; }
        }
        
        public override void Build(bool forContent = false)
        {
            BuildCondition();
            if (!string.IsNullOrEmpty(Condition))
                Sql.SqlClause = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", Connection.Driver.FormatTable(EntityObject.TableName), Condition);
            else
                Sql.SqlClause = string.Format("SELECT COUNT(*) FROM {0}", Connection.Driver.FormatTable(EntityObject.TableName));
        }

        public void Execute()
        {
            Build();
            returnCode = (int)Connection.QueryScalar(Sql);
        }
    }

    public class ListSelectHandle<T> : OperateHandle
    {
        private int from;

        public int From
        {
            get { return from; }
            set { from = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string orders;

        public string Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        private Order[] orderList;

        public Order[] OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }
        

        private DataTable table;

        public DataTable Table
        {
            get { return table; }
        }

        private List<T> data;

        public List<T> Data
        {
            get { return data; }
        }

        public ListSelectHandle()
        {
            data = new List<T>();
            table = new DataTable();
        }

        public override void Build(bool forContent = false)
        {
            if (forContent)
            {
                BuildFields(true, true);
                BuildOrders(true);
            }
            else
            {
                BuildFields(true);
                BuildOrders();
            }
            BuildCondition();
            Sql.SqlClause = Connection.Driver.BuildPaging(EntityObject.TableName, Fields, Condition, Orders, from, count);
        }

        public void BuildOrders(bool forContent = false)
        {
            StringBuilder s = new StringBuilder();
            foreach (Order o in orderList)
            {
                if (forContent)
                {
                    if (!ConListFieldDict.ContainsKey(o.Field))
                        throw new Exception(string.Format("unknown fieldValues {0} to table {1}", o.Field, EntityObject.TableName));
                    s.AppendFormat("{0} {1},", Connection.Driver.FormatField(o.Adorn, o.AliasField, o.Start, o.Length), o.Mode == OrderMode.Asc ? "ASC" : "DESC");
                }
                else
                {
                    if (!EntityObject.PropertyDict.ContainsKey(o.Field))
                        throw new Exception(string.Format("unknown fieldValues {0} to table {1}", o.Field, EntityObject.TableName));
                    s.AppendFormat("{0} {1},", Connection.Driver.FormatField(o.Adorn, o.Field, o.Start, o.Length), o.Mode == OrderMode.Asc ? "ASC" : "DESC");
                }
            }

            if (s.Length == 0 && from <= 0)
            {
                EntityObject.BuildOrders();
            }
            s.Length -= 1;
            orders = s.ToString();
        }

        public void Execute(bool forContent = false)
        {
            if (forContent)
            {
                Build(true);
                table = Connection.Query(Sql);
            }
            {
                Build();
                DataTable dt = Connection.Query(Sql);
                if (EntityObject.IsDataTable)
                {
                }
                else
                { 
                    foreach (DataRow r in dt.Rows)
                    {
                        object o = Activator.CreateInstance(EntityObject.ObjType);
                        BindObject(o, r);
                        data.Add((T)o);
                    }
                }
            }
        }

    }
}
