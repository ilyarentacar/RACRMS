using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Abstract
{
    public interface ICarRentalPriceVL
    {
        Task IsThereDateRange(int carId, DateTime startDate, DateTime endDate);
        void DateRangeCheck(DateTime startDate, DateTime endDate);
        Task IsThereAnyCarRentalPrice(CarRentalPriceDTO dto);
    }
}
