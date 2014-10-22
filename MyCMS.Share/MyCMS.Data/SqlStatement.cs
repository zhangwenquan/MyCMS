using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public class SqlStatement
    {
        string sqlClause;
        List<DataParameter> parameters;
        CommandType type;

        public SqlStatement()
        {
            type = CommandType.Text;
            parameters = new List<DataParameter>();
        }
        
        public CommandType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string SqlClause
        {
            get
            {
                return sqlClause;
            }
            set
            {
                sqlClause = value;
            }
        }

        public List<DataParameter> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

        public SqlStatement(string sql)
        {
            sqlClause = sql;
        }
    }
}
