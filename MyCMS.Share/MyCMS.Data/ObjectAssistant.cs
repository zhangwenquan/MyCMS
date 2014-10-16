using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class ObjectAssistant
    {
        private Dictionaries _d1;

        public Dictionary<string, ObjectManager> DicForTable()
        {
            return _d1.ObjColumnDic;
        }

        public void LoadDataSource(string dir)
        {
            _d1.LoadDataSource(dir, null);
        }

        public void LoadDataSource(string dir, string[] dbs)
        {
            _d1.LoadDataSource(dir, dbs);
        }

        public void LoadDBConnectionString(string connStr, string dbDriver)
        {
            _d1.SetGlobalDBString(connStr, dbDriver);
        }

        public Dictionary<string, IDatabase> GetDatabases()
        {
            return _d1.DatabaseDict;
        }

        public Dictionary<Type, ObjectManager> GetObjects()
        {
            return _d1.ObjectManagerDict;
        }

        public Dictionary<IDatabase, IConnection> GetConnections()
        {
            return _d1.ConnectionDict;
        }

        public IConnection CreateConnection(Type key, bool isTransfer = false)
        {
            var dic = _d1.ObjectManagerDict[key];
            IConnection conn = dic != null ? dic.CurDatabase.Driver.CreateConnection(dic.CurDatabase.ConnectionString) : null;
            if (conn != null)
            {
                conn.IsTransaction = isTransfer;
            }

            return conn;
        }

        public IConnection CreateConnection(string key, bool isTransfer = false)
        {
            var dic = _d1.ObjColumnDic[key];
            IConnection conn = dic != null ? dic.CurDatabase.Driver.CreateConnection(dic.CurDatabase.ConnectionString) : null;
            if (conn != null)
            {
                conn.IsTransaction = isTransfer;
            }

            return conn;
        }

        public object Insert(IConnection conn, object obj, string[] fields, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public object Select(IConnection conn, object obj, string[] fields, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(IConnection conn, object obj, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public int Update(IConnection conn, object obj, string[] fields, string tablename, Criteria condition)
        {
            throw new System.NotImplementedException();
        }

        public int Count<T>(IConnection conn, Criteria condition, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public void List<T>(IConnection conn, string condition, Order[] orders, string[] fields, int from, int count, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteList<T>(IConnection conn, Criteria conditino, string tablename)
        {
            throw new System.NotImplementedException();
        }
    }
}
