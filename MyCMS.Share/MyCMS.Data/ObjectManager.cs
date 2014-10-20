using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public class ObjectManager
    {
        EntityObject curObject;
        public EntityObject CurObject
        {
            get
            {
                return curObject;
            }
            set
            {
                curObject = value;
            }
        }

        Database curDatabase;
        public Database CurDatabase
        {
            get
            {
                return curDatabase;
            }
            set
            {
                curDatabase = value;
            }
        }

        Type objType;
        public Type ObjType
        {
            get
            {
                return objType;
            }
            set
            {
                objType = value;
            }
        }

        public int MyInsert(IConnection conn, object o, string[] fields, out object identity)
        {
            throw new System.NotImplementedException();
        }

        public int MyUpdate(IConnection conn, object o, string[] fields, Criteria condition)
        {
            throw new System.NotImplementedException();
        }

        public int MyDelete(IConnection conn, object o)
        {
            throw new System.NotImplementedException();
        }

        public int MyDeleteList(IConnection conn, Criteria condition)
        {
            throw new System.NotImplementedException();
        }

        public void MySelect(IConnection conn, object o, string[] fields)
        {
            throw new System.NotImplementedException();
        }

        public int MyCount(IConnection conn, Criteria condition)
        {
            throw new System.NotImplementedException();
        }

        public List<T> MyList<T>(IConnection conn, string[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            throw new System.NotImplementedException();
        }

        public List<T> MyList<T>(IConnection conn, ListField[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            throw new System.NotImplementedException();
        }

        public DataTable MyList<T>(IConnection conn, ConListField[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            throw new System.NotImplementedException();
        }
    }
}
