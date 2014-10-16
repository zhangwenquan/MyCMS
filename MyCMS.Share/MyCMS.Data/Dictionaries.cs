using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyCMS.Data
{
    public class Dictionaries
    {
        Dictionary<string, IDatabase> databaseDict;
        Dictionary<IDatabase, IConnection> connectionDict;
        Dictionary<string, ObjectManager> objColumnDic;
        Dictionary<Type, ObjectManager> objectManager;

        string GlobalDBString;
        string GlobalDBDriver;

        public Dictionaries()
        {
            databaseDict = new Dictionary<string, IDatabase>();
            connectionDict = new Dictionary<IDatabase, IConnection>();
            objColumnDic = new Dictionary<string, ObjectManager>();
            objectManager = new Dictionary<Type, ObjectManager>();
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
                return objectManager;
            }
        }
    
        public void LoadDatabases(string filename)
        {
            LoadDatabases(filename, null);
        }

        public void LoadDatabases(string filename, string[] dbs)
        {
            
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
