using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class EntityObject
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
    }
}
