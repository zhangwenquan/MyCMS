using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class Database : IDatabase
    {
        private IDbDriver dbDriver;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string driver;

        public string DriverStr
        {
            get { return driver; }
            set { driver = value; }
        }
        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public IDbDriver Driver
        {
            get { return dbDriver; }
            set { dbDriver = value; }
        }

        public IConnection CreateConnection()
        {
            return dbDriver.CreateConnection(connectionString);
        }

        private Dictionary<string, EntityObject> entityObjs;

        public Dictionary<string, EntityObject> EntityObjs
        {
            get { return entityObjs; }
        }
    }
}
