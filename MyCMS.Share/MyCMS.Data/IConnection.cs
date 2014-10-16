using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public interface IConnection
    {
        IDbDriver Driver
        {
            get;
            set;
        }

        bool IsTransaction
        {
            get;
            set;
        }

        DataTable Query(SqlStatement sql);

        object QueryScalar(SqlStatement sql);

        int Update(SqlStatement sql);

        void Commit();

        void Rollback();

        bool TableExists(string tablename);
    }
}
