using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class DataException : Exception
    {

        private ErrorCodes errorCode;

        public DataException(ErrorCodes code)
        {
            errorCode = code;
        }

        public DataException(string message, ErrorCodes code):base(message)
        {
            errorCode = code;
        }

        public DataException(string message, Exception ie, ErrorCodes code):base(message, ie)
        {
            errorCode = code;
        }

        public ErrorCodes ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
    }
}
