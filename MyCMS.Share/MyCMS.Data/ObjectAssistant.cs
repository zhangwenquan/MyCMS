using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class ObjectAssistant
    {
        private Dictionaries _d1;

        public void DicForTable()
        {
            throw new System.NotImplementedException();
        }

        public void LoadDataSource(string dir)
        {
            throw new System.NotImplementedException();
        }

        public void LoadDataSource(string dir, string[] dbs)
        {
            throw new System.NotImplementedException();
        }

        public void LoadDBConnectionString(string connStr, string dbDriver)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, IDatabase> GetDatabases()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<Type, ObjectManager> GetObjects()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<IDatabase, IConnection> GetConnections()
        {
            throw new System.NotImplementedException();
        }

        public IConnection CreateConnection(Type key, bool isTransfer = false)
        {
            throw new System.NotImplementedException();
        }

        public IConnection CreateConnection(string key, bool isTransfer = false)
        {
            throw new System.NotImplementedException();
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
