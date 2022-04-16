using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICarModelVL
    {
        Task IsThereAnyCarModel(CarModelDTO dto);
        Task IsThereName(int carBrandId, string name);
    }
}
