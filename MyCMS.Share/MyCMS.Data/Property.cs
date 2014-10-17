using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml;

namespace MyCMS.Data
{
    public class Property
    {
        private string field;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private int scale;

        public int Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private bool nullable;

        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }
        private bool _readonly;

        public bool Readonly
        {
            get { return _readonly; }
            set { _readonly = value; }
        }
        private PropertyInfo info;

        public PropertyInfo Info
        {
            get { return info; }
            set { info = value; }
        }
        private DbType type;

        public DbType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Property()
        {
            field = string.Empty;
            name = string.Empty;
            type = DbType.String;
            size = 10;
            scale = 0;
            nullable = true;
            description = string.Empty;
        }

        public void FromXml(XmlElement e)
        {
            string temp = string.Empty;
            field = e.GetAttribute("field");
            name = e.GetAttribute("name");
            description = e.GetAttribute("description");
            temp = e.GetAttribute("size");
            size = temp == string.Empty ? 0 : Convert.ToInt32(temp);
            temp = e.GetAttribute("scale");
            scale = temp == string.Empty ? 0 : Convert.ToInt32(scale);
            temp = e.GetAttribute("nullable");
            nullable = temp == string.Empty ? true : Convert.ToBoolean(temp);
            type = (DbType)Enum.Parse(typeof(DbType), e.GetAttribute("type"), true);
        }
    }
}
