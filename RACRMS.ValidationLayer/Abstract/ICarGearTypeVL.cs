using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICarGearTypeVL
    {
        Task IsThereName(string name);
    }
}
