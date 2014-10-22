using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public enum ErrorCodes
    {
        Success = 0,
        UnknownCriteria = 1,

        DriverRequired = 10,
        DriverFailed = 11,
        ConnectionRequired = 12,

        UnknownProperty = 20,
        UnknownObject = 21,
        ConditionRequired = 22,
        UnMatchType = 23,

        UnknownDatabase = 30,
        NoPrimaryKey = 31,
        NoData = 32
    }
}
