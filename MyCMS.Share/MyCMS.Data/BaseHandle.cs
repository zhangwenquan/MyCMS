using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public abstract class BaseHandle
    {
        private object executeObject;
        private string condition;
        private object entityObject;
        private Criteria criteria;
        private IConnection connection;
        private int parametersCount;

        public void BuildCondition(Criteria condition)
        {
            throw new System.NotImplementedException();
        }
    }
}
