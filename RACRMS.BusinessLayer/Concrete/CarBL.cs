using Microsoft.EntityFrameworkCore;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.DataTransferObject;
using RACRMS.Entity;
using RACRMS.Extension;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using RACRMS.ValidationLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Concrete
{
    public class CarBL : ICarBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Car car = await getById(id);

                if (car != null)
                {
                    unitOfWork.Car.Delete(car);

                    return await unitOfWork.SaveChangesAsync();
                }
                else
                    throw new Exception("Kayıt bulunamadı.");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CarDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Car.Select()
                    .Include(x => x.CarClass)
                    .Include(x => x.CarType)
                    .Include(x => x.CarBrand)
                    .Include(x => x.CarModel)
                    .Include(x => x.CarChassisType)
                    .Include(x => x.CarFuelType)
                    .Include(x => x.CarGearType)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Car.Select(x => x.Id == id)
                    .Include(x => x.CarClass)
                    .Include(x => x.CarType)
                    .Include(x => x.CarBrand)
                    .Include(x => x.CarModel)
                    .Include(x => x.CarChassisType)
                    .Include(x => x.CarFuelType)
                    .Include(x => x.CarGearType)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarDTO dto)
        {
            try
            {
                await plateNumberValidation(dto.PlateNumber);

                Car car = new Car()
                {
                    CarClassId = dto.CarClassId,
                    CarTypeId = dto.CarTypeId,
                    CarBrandId = dto.CarBrandId,
                    CarModelId = dto.CarModelId,
                    CarChassisTypeId = dto.CarChassisTypeId,
                    CarFuelTypeId = dto.CarFuelTypeId,
                    CarGearTypeId = dto.CarGearTypeId,
                    Rentable = dto.Rentable,
                    PlateNumber = dto.PlateNumber,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.Car.InsertAsync(car);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarDTO dto)
        {
            try
            {
                await isThereAnyCarValidation(dto);

                Car car = await getById(dto.Id);

                if (car == null)
                    throw new Exception("Kayıt bulunamadı.");

                car.CarClassId = dto.CarClassId;
                car.CarTypeId = dto.CarTypeId;
                car.CarBrandId = dto.CarBrandId;
                car.CarModelId = dto.CarModelId;
                car.CarChassisTypeId = dto.CarChassisTypeId;
                car.CarFuelTypeId = dto.CarFuelTypeId;
                car.CarGearTypeId = dto.CarGearTypeId;
                car.Rentable = dto.Rentable;
                car.PlateNumber = dto.PlateNumber;
                car.UpdateDate = DateTime.Now;

                unitOfWork.Car.Update(car);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Car> getById(int id)
        {
            try
            {
                return await unitOfWork.Car.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task plateNumberValidation(string plateNumber)
        {
            try
            {
                ICarVL carVL = new CarVL();

                carVL.PlateNumberValid(plateNumber);

                await carVL.IsTherePlateNumber(plateNumber);
            }
            catch
            {
                throw;
            }
        }

        private async Task isThereAnyCarValidation(CarDTO dto)
        {
            try
            {
                ICarVL carVL = new CarVL();

                carVL.PlateNumberValid(dto.PlateNumber);

                await carVL.IsThereAnyCar(dto);
            }
            catch
            {
                throw;
            }
        }
    }
}
