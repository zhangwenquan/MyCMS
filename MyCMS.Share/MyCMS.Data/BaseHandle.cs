using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public abstract class BaseHandle
    {
        object executeObject;

        public object ExecuteObject
        {
            get { return executeObject; }
            set { executeObject = value; }
        }
        string condition;

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        EntityObject entityObject;

        public EntityObject EntityObject
        {
            get { return entityObject; }
            set { entityObject = value; }
        }
        Criteria criteria;

        public Criteria Criteria
        {
            get { return criteria; }
            set { criteria = value; }
        }
        IConnection connection;

        public IConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        int parametersCount;

        public int ParametersCount
        {
            get { return parametersCount; }
            set { parametersCount = value; }
        }
        SqlStatement sql;

        public SqlStatement Sql
        {
            get { return sql; }
            set { sql = value; }
        }

        protected string Prefix
        {
            get { return connection.Driver.Prefix; }
        }

        public BaseHandle()
        {
            sql = new SqlStatement();
        }

        public void BuildCondition(Criteria c)
        {
            if (c != null)
            {
                condition = MakeCondition(c);
            }
        }

        public abstract void Build(bool forContent = false) ;

        public string AddParameter(Property p, object value)
        {
            string parameter = string.Format("{0}P{1}", Prefix, parametersCount++);
            DataParameter dp = new DataParameter();

            dp.ParameterName = p.Name;
            dp.Value = value;
            dp.Scale = p.Scale;
            dp.Size = p.Size;
            dp.SourceColumn = p.Field;
            dp.IsNullable = p.Nullable;
            dp.DbType = p.Type;

            sql.Parameters.Add(dp);

            return parameter;
        }

        /// <summary>
        /// 注意过滤条件中的字段应在表中存在，而propertyDict包含了表中的所有字段
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string MakeCondition(Criteria condition)
        {
            StringBuilder s = new StringBuilder();
            if (condition.Type != CriteriaType.None)
            {
                if (!EntityObject.PropertyDict.ContainsKey(condition.Field))
                {
                    throw new Exception("No such conListFieldDict in object. " + condition.Field);
                }
                if (condition.Type == CriteriaType.IsNull)
                    s.AppendFormat("{0} IS NULL", Connection.Driver.FormatField(condition.Adorn, condition.Field, condition.Start, condition.Length));
                else if (condition.Type == CriteriaType.IsNotNull)
                    s.AppendFormat("{0} IS NOT NULL", Connection.Driver.FormatField(condition.Adorn, condition.Field, condition.Start, condition.Length));
                else
                    s.AppendFormat("{0} {1} {2}", Connection.Driver.FormatField(condition.Adorn, condition.Field, condition.Start, condition.Length), Connection.Driver.GetCriteria(condition.Type), AddParameter(EntityObject.PropertyDict[condition.Field], condition.Value));
            }

            int num = condition.Criterias.Count;
            if (num > 0)
            {
                string mode = condition.Mode == CriteriaMode.And ? "AND" : "OR";
                s.AppendFormat(" {0} ", mode);
                if (num > 1)
                {
                    s.Append("(");
                }
                foreach (Criteria sub in condition.Criterias)
                {
                    s.AppendFormat("{0} {1} ", MakeCondition(sub), mode);
                }
                s.Length = s.Length - mode.Length - 2;
                if (num > 1)
                {
                    s.Append(")");
                }
            }

            return s.ToString();
        }
    }
}
