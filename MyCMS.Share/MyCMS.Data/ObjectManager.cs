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
            identity = null;
            InsertHandle ih = new InsertHandle();
            ih.EntityObject = CurObject;
            ih.ExecuteObject = o;
            ih.Connection = conn;
            ih.BuildFieldsDicts(fields);
            ih.Execute();
            identity = ih.ReturnObj;
            return ih.ReturnCode;
        }

        public int MyUpdate(IConnection conn, object o, string[] fields, Criteria condition)
        {
            UpdateHandle uh = new UpdateHandle();
            uh.EntityObject = CurObject;
            uh.ExecuteObject = o;
            uh.Criteria = condition;
            uh.BuildFieldsDicts(fields);
            uh.Connection = conn;
            uh.Execute();

            return uh.ReturnCode;
        }

        public int MyDelete(IConnection conn, object o)
        {
            return MyDeleteList(conn, CurObject.BuildCriteria(o));
        }

        public int MyDeleteList(IConnection conn, Criteria condition)
        {
            DeleteHandle dh = new DeleteHandle();
            dh.EntityObject = CurObject;
            dh.Criteria = condition;
            dh.Connection = conn;
            dh.Execute();

            return dh.ReturnCode;
        }

        public void MySelect(IConnection conn, object o, string[] fields)
        {
            SelectHandle sh = new SelectHandle();
            sh.Connection = conn;
            sh.Criteria = CurObject.BuildCriteria(o);
            sh.EntityObject = CurObject;
            sh.ExecuteObject = o;
            sh.BuildFieldsDicts(fields);
            sh.Execute();
        }

        public int MyCount(IConnection conn, Criteria condition)
        {
            CountHandle ch = new CountHandle();
            ch.Criteria = condition;
            ch.Connection = conn;
            ch.EntityObject = CurObject;
            ch.Execute();

            return ch.ReturnCode;
        }

        public List<T> MyList<T>(IConnection conn, string[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            ListSelectHandle<T> lsh = new ListSelectHandle<T>();
            lsh.Connection = conn;
            lsh.Criteria = condition;
            lsh.From = from;
            lsh.Count = count;
            lsh.OrderList = orders;
            lsh.EntityObject = curObject;
            lsh.BuildFieldsDicts(fields);
            lsh.Execute();

            return lsh.Data;
        }

        public List<T> MyList<T>(IConnection conn, ListField[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            ListSelectHandle<T> lsh = new ListSelectHandle<T>();
            lsh.Connection = conn;
            lsh.Criteria = condition;
            lsh.From = from;
            lsh.Count = count;
            lsh.OrderList = orders;
            lsh.EntityObject = curObject;
            lsh.BuildFieldsDicts(fields);
            lsh.Execute();

            return lsh.Data;
        }

        public DataTable MyList<T>(IConnection conn, ConListField[] fields, Criteria condition, int from, int count, Order[] orders)
        {
            ListSelectHandle<T> lsh = new ListSelectHandle<T>();
            lsh.Connection = conn;
            lsh.Criteria = condition;
            lsh.From = from;
            lsh.Count = count;
            lsh.OrderList = orders;
            lsh.EntityObject = curObject;
            lsh.BuildFieldsDicts(fields);
            lsh.Execute();

            return lsh.Table;
        }
    }
}
