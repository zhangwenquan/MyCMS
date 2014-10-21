using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class OperateHandle : BaseHandle
    {
        private string fields;

        public string Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        private string orders;

        public string Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        private Dictionary<string, ListField> listFieldDict;

        public Dictionary<string, ListField> ListFieldDict
        {
            get { return listFieldDict; }
            set { listFieldDict = value; }
        }
        private Dictionary<string, ConListField> conListFieldDict;

        public Dictionary<string, ConListField> ConListFieldDict
        {
            get { return conListFieldDict; }
            set { conListFieldDict = value; }
        }

        Order[] orderList;

        public Order[] OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

        public OperateHandle()
        {
            listFieldDict = new Dictionary<string, ListField>(StringComparer.OrdinalIgnoreCase);
            conListFieldDict = new Dictionary<string, ConListField>();
        }

        public override void Build(bool forContent)
        {
            throw new NotImplementedException();
        }

        public void BuildFields(bool readonlyAllowed, bool forContent = false)
        {
            StringBuilder s = new StringBuilder();
            if (forContent)
            {
                foreach (KeyValuePair<string, ConListField> pair in conListFieldDict)
                {
                    s.Append(Connection.Driver.FormatField(pair.Value)+",");
                }
            }
            else
            {
                foreach (Property p in EntityObject.PropertyDict.Values)
                {
                    if (!readonlyAllowed && p.Readonly)
                        continue;
                    if (listFieldDict.ContainsKey(p.Name))
                    {
                        ListField field = listFieldDict[p.Name];
                        s.Append(Connection.Driver.FormatField(field.Adorn, field.Fieldname)+",");
                    }
                }
            }

            s.Length = s.Length - 1;
            fields = s.ToString();
        }

        public void BuildFieldsDicts(IEnumerable fields)
        {
            foreach (var field in fields)
            {
                if (string.Compare(field.GetType().Name, "string", true) == 0)
                {
                    string listField = field as string;
                    listFieldDict.Add(listField, new ListField(listField));
                }
                else if (string.Compare(field.GetType().Name, "ListField", true) == 0)
                {
                    ListField listField = field as ListField;
                    listFieldDict.Add(listField.Fieldname, listField);
                }
                else if (string.Compare(field.GetType().Name, "ConListField", true) == 0)
                {
                    ConListField conListField = field as ConListField;
                    conListFieldDict.Add(conListField.Field, conListField);
                }
            }
        }

        public void BuildOrders(bool forContent = false)
        {
            StringBuilder s = new StringBuilder();
            foreach (Order o in orderList)
            {
                if (forContent)
                {
                    if (!conListFieldDict.ContainsKey(o.Field))
                        throw new Exception(string.Format("unknown fieldValues {0} to table {1}", o.Field, EntityObject.TableName));
                    s.AppendFormat("{0} {1},", Connection.Driver.FormatField(o.Adorn, o.AliasField, o.Start, o.Length), o.Mode == OrderMode.Asc ? "ASC" : "DESC");
                }
                else
                {
                    if (!listFieldDict.ContainsKey(o.Field))
                        throw new Exception(string.Format("unknown fieldValues {0} to table {1}", o.Field, EntityObject.TableName));
                    s.AppendFormat("{0} {1},", Connection.Driver.FormatField(o.Adorn, o.Field, o.Start, o.Length), o.Mode == OrderMode.Asc ? "ASC" : "DESC");
                }
            }
            s.Length -= 1;
            orders = s.ToString();
        }
    }
}
