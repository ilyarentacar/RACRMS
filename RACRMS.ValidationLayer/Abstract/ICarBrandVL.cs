using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICarBrandVL
    {
        Task IsThereName(string name);
    }
}
