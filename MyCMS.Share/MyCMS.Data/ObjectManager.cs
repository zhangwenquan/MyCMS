using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class ObjectManager
    {
        private int GlobalDBString;
        private string GlobalDBDriver;
    
        public EntityObject CurObject
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IDatabase CurDatabase
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Type ObjType
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
