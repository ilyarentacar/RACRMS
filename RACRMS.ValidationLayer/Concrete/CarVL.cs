using Microsoft.EntityFrameworkCore;
using RACRMS.DataTransferObject;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Concrete
{
    public class CarVL : ICarVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereAnyCar(CarDTO dto)
        {
            try
            {
                if (await unitOfWork.Car.Select()
                    .Where(x => x.PlateNumber == dto.PlateNumber)
                    .Where(x => x.CarClassId == dto.CarClassId)
                    .Where(x => x.CarTypeId == dto.CarTypeId)
                    .Where(x => x.CarBrandId == dto.CarBrandId)
                    .Where(x => x.CarModelId == dto.CarModelId)
                    .Where(x => x.CarChassisTypeId == dto.CarChassisTypeId)
                    .Where(x => x.CarFuelTypeId == dto.CarFuelTypeId)
                    .Where(x => x.CarGearTypeId == dto.CarGearTypeId)
                    .Where(x => x.Rentable == dto.Rentable)
                    .AnyAsync())
                    throw new Exception("Bu araç zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }

        public async Task IsTherePlateNumber(string plateNumber)
        {
            try
            {
                if (await unitOfWork.Car.Select(x => x.PlateNumber == plateNumber).AnyAsync())
                    throw new Exception("Bu plaka numarası başka bir araç tarafından kullanılmaktadır.");
            }
            catch
            {
                throw;
            }
        }

        public void PlateNumberValid(string plateNumber)
        {
            try
            {
                Regex regex = new Regex("(0[1-9]|[1-7][0-9]|8[01])(([A-Z])(\\d{4,5})|([A-Z]{2})(\\d{3,4})|([A-Z]{3})(\\d{2}))");

                if (!regex.IsMatch(plateNumber))
                    throw new Exception("Lütfen geçerli bir plaka numarası giriniz.");
            }
            catch
            {
                throw;
            }
        }
    }
}
