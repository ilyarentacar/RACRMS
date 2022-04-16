using RACRMS.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Common.Abstract.RequestType
{
    public interface IBaseRequestData
    {
        RequestTypeEnum RequestType { get; set; }
        object Model { get; set; }
    }
}
