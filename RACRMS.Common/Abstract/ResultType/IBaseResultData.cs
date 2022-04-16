using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Common.Abstract.ResultType
{
    public interface IBaseResultData
    {
        bool HasError { get; set; }
        string ErrorMessage { get; set; }
        object Data { get; set; }
    }
}
