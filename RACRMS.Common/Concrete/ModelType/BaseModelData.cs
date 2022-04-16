using RACRMS.Common.Abstract.ModelType;
using RACRMS.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Common.Concrete.ModelType
{
    public class BaseModelData : IBaseModelData
    {
        public RequestTypeEnum RequestType { get; set; }
        public object Model { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
