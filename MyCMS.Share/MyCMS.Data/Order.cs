using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class Order
    {
        string field;
        public string Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
            }
        }

        OrderMode mode;
        public OrderMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        Adorns adorn;
        public Adorns Adorn
        {
            get
            {
                return adorn;
            }
            set
            {
                adorn = value;
            }
        }

        string aliasField;
        public string AliasField
        {
            get
            {
                return aliasField;
            }
            set
            {
                aliasField = value;
            }
        }

        int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
