using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace MyCMS.Data
{
    public class Dictionaries
    {
        Dictionary<string, IDatabase> databaseDict;
        Dictionary<IDatabase, IConnection> connectionDict;
        Dictionary<string, ObjectManager> objColumnDic;
        Dictionary<Type, ObjectManager> objectManagerDict;

        string GlobalDBString;
        string GlobalDBDriver;

        public Dictionaries()
        {
            databaseDict = new Dictionary<string, IDatabase>();
            connectionDict = new Dictionary<IDatabase, IConnection>();
            objColumnDic = new Dictionary<string, ObjectManager>();
            objectManagerDict = new Dictionary<Type, ObjectManager>();
        }
        /// <summary>
        /// 每个数据库对应的Connection都不同，因此有必要封装每个Connection到字典表
        /// </summary>
        public Dictionary<string, IDatabase> DatabaseDict
        {
            get
            {
                return databaseDict;
            }
        }

        public Dictionary<IDatabase, IConnection> ConnectionDict
        {
            get
            {
                return connectionDict;
            }
        }

        public Dictionary<string, ObjectManager> ObjColumnDic
        {
            get
            {
                return objColumnDic;
            }
            set 
            {
                objColumnDic = value;
            }
        }

        public Dictionary<Type, ObjectManager> ObjectManagerDict
        {
            get
            {
                return objectManagerDict;
            }
        }
    
        public void LoadDatabases(string filename)
        {
            
        }

        public void LoadDatabases(string filename, string[] dbs)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlElement element in doc.SelectNodes("/Objects/Database"))
            {
                Database database = new Database();
                database.FromXml(element);

                if (dbs != null && !InArray(database.Name, dbs))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(GlobalDBString))
                {
                    database.ConnectionString = GlobalDBString.Replace("{$App}", AppDomain.CurrentDomain.BaseDirectory);
                    database.DriverStr = GlobalDBDriver.Replace("{$Current}", Path.GetDirectoryName(filename));
                }

                databaseDict.Add(database.Name, database);
                connectionDict.Add(database, database.CreateConnection());

                foreach (EntityObject entity2Table in database.EntityObjs.Values)
                {
                    //entity2Table.Build();
                    EntityObject copyEntity = entity2Table.Clone() as EntityObject;
                    

                    ObjectManager om = new ObjectManager();
                    entity2Table.IsDataTable = false;
                    entity2Table.TypeForDT = null;
                    om.CurDatabase = database;
                    om.CurObject = entity2Table;
                    om.ObjType = entity2Table.ObjType;
                    //objectManagerDict.Add(entity2Table.ObjType, om);

                    //copyEntity.Build();
                    om = new ObjectManager();
                    copyEntity.IsDataTable = true;
                    copyEntity.TypeForDT = typeof(TableInfo);
                    copyEntity.ObjType = typeof(TableInfo);
                    om.CurDatabase = database;
                    om.CurObject = copyEntity;
                    om.ObjType = copyEntity.ObjType;
                    objColumnDic.Add(copyEntity.TableName, om);
                }
            }
        }

        bool InArray(string name, string[] dbs)
        {
            foreach (string db in dbs)
            {
                if (string.Compare(name, db, true) == 0)
                    return true;
            }
            return false;
        }

        public void LoadDataSource(string root, string[] dbs)
        {
            if (Directory.Exists(root))
            {
                DirectoryInfo rootDir = new DirectoryInfo(root);
                FileInfo[] files = rootDir.GetFiles("*.xml");
                foreach (FileInfo file in files)
                {
                    LoadDatabases(file.FullName, dbs);
                }
            }
            else
                throw new Exception("目录不存在！");
        }

        public IConnection CreateConnection(string database)
        {
            return DatabaseDict[database].CreateConnection();
        }

        public IConnection GetDBConnection(string database)
        {
            return ConnectionDict[GetDatabase(database)];
        }

        public IDatabase GetDatabase(string database) 
        {
            if (!DatabaseDict.ContainsKey(database))
                throw new Exception("UnknownDatabase");
            else
                return DatabaseDict[database];
        }

        public void SetGlobalDBString(string conStr, string driverStr)
        {
            GlobalDBString = conStr;
            GlobalDBDriver = driverStr;
        }
    }
}
