using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class Dictionaries
    {
        /// <summary>
        /// 每个数据库对应的Connection都不同，因此有必要封装每个Connection到字典表
        /// </summary>
        public Dictionary<string, IDatabase> DatabaseDict
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Dictionary<IDatabase, IConnection> ConnectionDict
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Dictionary<string, ObjectManager> ObjColumnDic
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Dictionary<Type, ObjectManager> ObjectManagerDict
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public void LoadDatabases(string filename)
        {
            throw new System.NotImplementedException();
        }

        public void LoadDatabases(string filename, string[] dbs)
        {
            throw new System.NotImplementedException();
        }

        public void LoadDataSource(string root, string[] dbs)
        {
            throw new System.NotImplementedException();
        }

        public IConnection CreateConnection(string database)
        {
            throw new System.NotImplementedException();
        }

        public IConnection GetDBConnection(string database)
        {
            throw new System.NotImplementedException();
        }
    }
}
