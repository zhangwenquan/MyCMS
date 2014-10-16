using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public interface IConnectionEx : IConnection
    {
        string ConnectionString
        {
            get;
            set;
        }

        bool Create
        {
            get;
            set;
        }
    }
}
