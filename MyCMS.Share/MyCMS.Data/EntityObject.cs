using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace MyCMS.Data
{
    public class EntityObject : ICloneable
    {
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private string primaryKey;

        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        private string typeName;

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        private Type objType;

        public Type ObjType
        {
            get { return objType; }
            set { objType = value; }
        }
        private bool isDataTable = false;

        public bool IsDataTable
        {
            get { return isDataTable; }
            set { isDataTable = value; }
        }
        private Type typeForDT;

        public Type TypeForDT
        {
            get { return typeForDT; }
            set { typeForDT = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string identityName;

        public string IdentityName
        {
            get { return identityName; }
            set { identityName = value; }
        }
        private Dictionary<string, Property> propertyDict;

        public Dictionary<string, Property> PropertyDict
        {
            get { return propertyDict; }
            set { propertyDict = value; }
        }

        public bool IsIdentity
        {
            get {
                return !string.IsNullOrEmpty(identityName);
            }
        }

        public EntityObject()
        {
            propertyDict = new Dictionary<string, Property>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void FromXml(XmlElement e)
        {
            tableName = e.GetAttribute("table");
            typeName = e.GetAttribute("type");
            primaryKey = e.GetAttribute("primaryKey");
            identityName = e.GetAttribute("identity");
            description = e.GetAttribute("description");

            foreach (XmlElement element in e.SelectNodes("Property"))
            {
                Property p = new Property();
                p.FromXml(element);
                propertyDict.Add(p.Name, p);
            }
        }

        public void Build()
        {
            objType = Type.GetType(typeName);
            if (objType == null)
                throw new Exception("UnknownObject");

            PropertyInfo[] pis = objType.GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                if (propertyDict.ContainsKey(pi.Name))
                {
                    propertyDict[pi.Name].Info = pi;
                }
            }

            foreach (Property p in PropertyDict.Values)
            {
                if (p.Info == null)
                    throw new Exception("Unknown Property");
            }
        }
    }
}
