using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public interface IDatabase
    {
        string ConnectionString
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        IDbDriver Driver
        {
            get;
            set;
        }

        IConnection CreateConnection();


    }
}
