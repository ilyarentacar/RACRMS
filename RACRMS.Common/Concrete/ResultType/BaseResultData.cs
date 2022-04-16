using RACRMS.Common.Abstract.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Common.Concrete.ResultType
{
    public class BaseResultData : IBaseResultData
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
