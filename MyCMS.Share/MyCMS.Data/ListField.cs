using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class ListField
    {
        private string fieldname;

        public string Fieldname
        {
            get { return fieldname; }
            set { fieldname = value; }
        }
        private Adorns adorn;

        public Adorns Adorn
        {
            get { return adorn; }
            set { adorn = value; }
        }

        public ListField()
        {
            adorn = Adorns.None;
        }

        public ListField(string field) : this()
        {
            fieldname = field;
        }
    }
}
