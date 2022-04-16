using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICarVL
    {
        void PlateNumberValid(string plateNumber);
        Task IsTherePlateNumber(string plateNumber);
        Task IsThereAnyCar(CarDTO dto);
    }
}
