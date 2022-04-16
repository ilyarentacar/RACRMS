using Microsoft.EntityFrameworkCore;
using RACRMS.DataTransferObject;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Concrete
{
    public class CarRentalPriceVL : ICarRentalPriceVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarRentalPriceVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public void DateRangeCheck(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate >= endDate)
                    throw new Exception("Başlangıç tarihi bitiş tarihine eşit veya büyük olamaz.");
            }
            catch
            {

                throw;
            }
        }

        public async Task IsThereAnyCarRentalPrice(CarRentalPriceDTO dto)
        {
            try
            {
                if (await unitOfWork.CarRentalPrice.Select()
                    .Where(x => x.CarId == dto.CarId)
                    .Where(x => x.StartDate == dto.StartDate)
                    .Where(x => x.EndDate == dto.EndDate)
                    .Where(x => x.RentPrice == dto.RentPrice)
                    .AnyAsync())
                    throw new Exception("Bu araç için, kiralama fiyatı zaten eklenmiştir.");
            }
            catch
            {
                throw;
            }
        }

        public async Task IsThereDateRange(int carId, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (await unitOfWork.CarRentalPrice.Select()
                    .Where(x => x.CarId == carId)
                    .Where(x => x.StartDate == startDate)
                    .Where(x => x.EndDate == endDate)
                    .AnyAsync())
                    throw new Exception("Bu araç için, bu tarih aralığında kiralama fiyatı zaten eklenmiştir.");
            }
            catch
            {
                throw;
            }
        }
    }
}
