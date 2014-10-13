using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        SqlStatement Query();

        SqlStatement QueryScalar();

        SqlStatement Update();

        void Commit();

        void Rollback();

        string TableExists();
    }
}
