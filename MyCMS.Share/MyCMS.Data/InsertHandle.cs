using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCMS.Data
{
    public class InsertHandle : OperateHandle
    {

        private string fieldValues;

        public string FieldValues
        {
            get { return fieldValues; }
        }
        private int returnCode;

        public int ReturnCode
        {
            get { return returnCode; }
        }
        private object returnObj;

        public object ReturnObj
        {
            get { return returnObj; }
        }

        void BuildFieldsValue()
        {
            StringBuilder s = new StringBuilder();
            foreach (Property p in EntityObject.PropertyDict.Values)
            {
                if (!ListFieldDict.ContainsKey(p.Name))
                    continue;
                s.Append(AddParameter(p, p.Info.GetValue(ExecuteObject)));
            }
        }
    }

    public class SelectHandle : OperateHandle
    {
    }

    public class UpdateHandle : OperateHandle
    {
    }

    public class DeleteHandle : BaseHandle
    {

        public override void Build(bool forContent = false)
        {
            throw new NotImplementedException();
        }
    }

    public class CountHandle : OperateHandle
    {
    }

    public class ListSelectHandle<T> : OperateHandle
    {
    }
}
