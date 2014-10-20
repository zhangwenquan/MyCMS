using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyCMS.Data
{
    public class DataParameter : IDbDataParameter
    {
        byte precision;
        public byte Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
            }
        }

        byte scale;
        public byte Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        int size;
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        DbType type;
        public DbType DbType
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

        ParameterDirection direction;
        public ParameterDirection Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        bool isNullable;
        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        string parameterName;
        public string ParameterName
        {
            get
            {
                return parameterName;
            }
            set
            {
                parameterName = value;
            }
        }

        string sourceColumn;
        public string SourceColumn
        {
            get
            {
                return sourceColumn;
            }
            set
            {
                sourceColumn = value;
            }
        }

        DataRowVersion srcVersion;
        public DataRowVersion SourceVersion
        {
            get
            {
                return srcVersion;
            }
            set
            {
                srcVersion = value;
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
    }
}
