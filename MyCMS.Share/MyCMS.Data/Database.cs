using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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
            get 
            {
                if (dbDriver == null)
                {
                    if (string.IsNullOrEmpty(driver))
                    {
                        throw new Exception("DriverRequired");
                    }
                    try
                    {
                        Type type = Type.GetType(driver);
                        dbDriver = (IDbDriver)Activator.CreateInstance(type);
                    }
                    catch
                    {
                        throw new Exception("UnknownDriver");
                    }
                }
                
                return dbDriver;
            }
            set { dbDriver = value; }
        }

        public IConnection CreateConnection()
        {
            return Driver.CreateConnection(connectionString);
        }

        private Dictionary<string, EntityObject> entityObjs;

        public Dictionary<string, EntityObject> EntityObjs
        {
            get { return entityObjs; }
        }

        public Database()
        {
            entityObjs = new Dictionary<string, EntityObject>();
        }

        public void FromXml(XmlElement e)
        {
            EntityObjs.Clear();
            name = e.GetAttribute("name");
            driver = e.GetAttribute("driver");
            connectionString = e.GetAttribute("connectionString");

            foreach (XmlElement element in e.SelectNodes("Object"))
            {
                EntityObject entity2Table = new EntityObject();
                entity2Table.FromXml(element);
                entityObjs.Add(entity2Table.TableName, entity2Table);
            }
        }
    }
}
