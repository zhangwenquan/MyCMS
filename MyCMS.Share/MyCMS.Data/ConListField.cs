using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class ConListField
    {
        private string field;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }
        private Adorns adorn;

        public Adorns Adorn
        {
            get { return adorn; }
            set { adorn = value; }
        }
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        private int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        private string aliasField;

        public string AliasField
        {
            get { return aliasField; }
            set { aliasField = value; }
        }
        private System.Data.DbType type;

        public System.Data.DbType Type
        {
            get { return type; }
            set { type = value; }
        }
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public ConListField()
        {
            Adorn = Adorns.None;
        }

        public ConListField(string field, string alias) : this()
        {
            this.field = field;
            this.aliasField = alias;
        }

        public ConListField(Adorns adorn, string field, string alias)
        {
            this.adorn = adorn;
            this.field = field;
            this.aliasField = alias;
        }
    }
}
