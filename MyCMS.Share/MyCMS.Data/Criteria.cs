using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class Criteria
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

        CriteriaType type;
        public CriteriaType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        CriteriaMode mode;
        public CriteriaMode Mode
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

        object objValue;
        public object Value
        {
            get
            {
                return objValue;
            }
            set
            {
                objValue = value;
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

        List<Criteria> criterias;
        public List<Criteria> Criterias
        {
            get
            {
                return criterias;
            }
            set
            {
                criterias = value;
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

        public Criteria()
        {
            criterias = new List<Criteria>();
            mode = CriteriaMode.And;
            type = CriteriaType.None;
        }

        public Criteria(CriteriaType type)
            : this()
        {
            this.type = type;
        }

        public Criteria(CriteriaType type, string name, object o)
            : this(type)
        {
            this.field = name;
            this.objValue = o;
        }
    }
}
