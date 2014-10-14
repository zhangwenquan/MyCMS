using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public enum CriteriaType
    {
        Equals,
        MoreThan,
        MoreThanEquals,
        LessThan,
        LessThanEquals,
        IsNull,
        IsNotNull,
        Like,
        NotLike,
        NotEquals,
        None,
    }
}
