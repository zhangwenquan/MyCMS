using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class OperateHandle : BaseHandle
    {
        private string fields;
        private string orders;
        private Dictionary<string, ListField> listFieldDict;
    }
}
