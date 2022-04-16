using RACRMS.Common.Abstract.RequestType;
using RACRMS.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Common.Concrete.RequestType
{
    public class BaseRequestData<T> : IBaseRequestData
    {
        public RequestTypeEnum RequestType { get; set; }
        public object Model { get; set; }
    }
}
