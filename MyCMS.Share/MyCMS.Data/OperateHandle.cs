using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public class OperateHandle : BaseHandle
    {
        private string fields;

        public string Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        private string orders;

        public string Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        private Dictionary<string, ListField> listFieldDict;

        public Dictionary<string, ListField> ListFieldDict
        {
            get { return listFieldDict; }
            set { listFieldDict = value; }
        }
        private Dictionary<string, ConListField> conListFieldDict;

        public Dictionary<string, ConListField> ConListFieldDict
        {
            get { return conListFieldDict; }
            set { conListFieldDict = value; }
        }

        Order[] orderList;

        public Order[] OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

        public OperateHandle()
        {
            listFieldDict = new Dictionary<string, ListField>(StringComparer.InvariantCultureIgnoreCase);
            conListFieldDict = new Dictionary<string, ConListField>(StringComparer.InvariantCultureIgnoreCase);
        }

        public override void Build(bool forContent = false)
        {
            
        }

        public void BuildFields(bool readonlyAllowed, bool forContent = false)
        {
            StringBuilder s = new StringBuilder();
            if (forContent)
            {
                foreach (KeyValuePair<string, ConListField> pair in conListFieldDict)
                {
                    s.Append(Connection.Driver.FormatField(pair.Value)+",");
                }
            }
            else
            {
                foreach (Property p in EntityObject.PropertyDict.Values)
                {
                    if (!readonlyAllowed && p.Readonly)
                        continue;
                    Adorns a = Adorns.None;
                    if (ListFieldDict.Count > 0)
                    {
                        if (ListFieldDict.ContainsKey(p.Name))
                            a = listFieldDict[p.Name].Adorn;
                    }
                    
                    s.Append(Connection.Driver.FormatField(a, p.Field) + ",");
                }
            }

            s.Length -= 1;
            fields = s.ToString();
        }

        public void BuildFieldsDicts(IEnumerable fields)
        {
            foreach (var field in fields)
            {
                if (string.Compare(field.GetType().Name, "string", true) == 0)
                {
                    string listField = field as string;
                    listFieldDict.Add(listField, new ListField(listField));
                }
                else if (string.Compare(field.GetType().Name, "ListField", true) == 0)
                {
                    ListField listField = field as ListField;
                    listFieldDict.Add(listField.Fieldname, listField);
                }
                else if (string.Compare(field.GetType().Name, "ConListField", true) == 0)
                {
                    ConListField conListField = field as ConListField;
                    conListFieldDict.Add(conListField.Field, conListField);
                }
            }
        }

        public void BindObject(object o, DataRow dr)
        {
            foreach (Property p in EntityObject.PropertyDict.Values)
            {
                if (ListFieldDict.Count > 0 &&
                    !ListFieldDict.ContainsKey(p.Field))
                    continue;
                object v = dr[p.Field];
                if (v == DBNull.Value)
                    v = null;
                p.Info.SetValue(o, v);
            }
        }
    }
}
