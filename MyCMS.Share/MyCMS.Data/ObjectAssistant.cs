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

        public object Insert(IConnection conn, object obj, string[] fields)
        {
            ObjectManager om = _d1.ObjectManagerDict[obj.GetType()];
            object identity = null;
            return om.MyInsert(conn, obj, fields, out identity);
        }

        public object Insert(IConnection conn, object obj, string[] fields, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            object identity = null;
            return om.MyInsert(conn, obj, fields, out identity);
        }

        public void Select(IConnection conn, object obj, string[] fields)
        {
            ObjectManager om = _d1.ObjectManagerDict[obj.GetType()];
            om.MySelect(conn, obj, fields);
        }

        public void Select(IConnection conn, object obj, string[] fields, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            om.MySelect(conn, obj, fields);
        }
        public bool Delete(IConnection conn, object obj)
        {
            ObjectManager om = _d1.ObjectManagerDict[obj.GetType()];
            return om.MyDelete(conn, obj) == 1;
        }

        public bool Delete(IConnection conn, object obj, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            return om.MyDelete(conn, obj) == 1;
        }

        public int Update(IConnection conn, object obj, string[] fields, Criteria condition)
        {
            ObjectManager om = _d1.ObjectManagerDict[obj.GetType()];
            return om.MyUpdate(conn, obj, fields, condition);
        }


        public int Update(IConnection conn, object obj, string[] fields, string tablename, Criteria condition)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            return om.MyUpdate(conn, obj, fields, condition);
        }

        public int Count<T>(IConnection conn, Criteria condition)
        {
            ObjectManager om = _d1.ObjectManagerDict[typeof(T)];
            return om.MyCount(conn, condition);
        }

        public int Count<T>(IConnection conn, Criteria condition, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            return om.MyCount(conn, condition);
        }

        public List<T> List<T>(IConnection conn, Criteria condition, Order[] orders, string[] fields, int from, int count, string tablename)
        {
            ObjectManager om = _d1.ObjectManagerDict[typeof(T)];
            return om.MyList<T>(conn, fields, condition, from, count, orders);
        }

        public List<T> List<T>(IConnection conn, Criteria condition, Order[] orders, string[] fields, int from, int count, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            return om.MyList<T>(conn, fields, condition, from, count, orders);
        }
        public int DeleteList<T>(IConnection conn, Criteria conditino)
        {
            ObjectManager om = _d1.ObjectManagerDict[typeof(T)];
            return om.MyDeleteList(conn, conditino);
        }


        public int DeleteList<T>(IConnection conn, Criteria conditino, string tablename)
        {
            ObjectManager om = _d1.ObjColumnDic[tablename];
            return om.MyDeleteList(conn, conditino);
        }
    }
}
