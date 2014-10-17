using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public interface IDbDriver
    {
        string Prefix
        {
            get;
        }
    
        string GetCriteria(CriteriaType type);

        string FormatField(Adorns adorn, string field);

        string FormatTable(string table);

        SqlStatement FormatSQL(SqlStatement sql);

        string BuildPaging(string tablename, string fields, string where, string orders, int from, int count);

        IConnection CreateConnection(string connectionString);

        IConnection CreateConnection(string connectionString, bool create);

        string FormatField(Adorns adorn, string field, int start, int length);

        string FormatField(ConListField field);
    }
}
