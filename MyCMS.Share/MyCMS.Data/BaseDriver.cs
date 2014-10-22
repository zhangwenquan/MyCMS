using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public abstract class BaseDriver : IDbDriver
    {
        public virtual string GetCriteria(CriteriaType type)
        {
            switch (type)
            { 
                case CriteriaType.Equals:
                    return "=";
                case CriteriaType.NotEquals:
                    return "<>";
                case CriteriaType.MoreThan:
                    return ">";
                case CriteriaType.MoreThanEquals:
                    return ">=";
                case CriteriaType.LessThan:
                    return "<";
                case CriteriaType.LessThanEquals:
                    return "<=";
                case CriteriaType.IsNull:
                    return "IS NULL";
                case CriteriaType.IsNotNull:
                    return "IS NOT NULL";
                case CriteriaType.Like:
                    return "LIKE";
                case CriteriaType.NotLike:
                    return "NOT LIKE";
                default:
                    throw new Exception("");
            }
        }

        public virtual string FormatTable(string table)
        {
            return string.Format("[{0}]", table);
        }

        public virtual SqlStatement FormatSQL(SqlStatement sql)
        {
            return sql;
        }

        public virtual string BuildPaging(string tablename, string fields, string where, string orders, int from, int count)
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("SELECT {0} FROM {1}", fields, tablename);
            if (!string.IsNullOrEmpty(where))
                s.AppendFormat(" WHERE {0}", where);
            if (!string.IsNullOrEmpty(orders))
                s.AppendFormat(" ORDER BY {0}", orders);
            if (from > 0)
                s.AppendFormat(" OFFSET {0} COUNT {1}", from, count);
            else if (count > 0)
                s.AppendFormat(" OFFSET 0 COUNT {0}", count);

            return s.ToString();
        }


        public abstract IConnection CreateConnection(string connectionString);

        public abstract IConnection CreateConnection(string connectionString, bool create);

        public virtual string Prefix
        {
            get
            {
                return "@";
            }
        }

        public abstract string FormatField(Adorns adorn, string field);

        public abstract string FormatField(Adorns adorn, string field, int start, int length);

        public abstract string FormatField(ConListField field);
    }
}
