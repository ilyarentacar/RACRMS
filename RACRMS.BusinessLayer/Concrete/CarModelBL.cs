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
    public class CarModelBL : ICarModelBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarModelBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarModel carModel = await getById(id);

                if (carModel != null)
                {
                    unitOfWork.CarModel.Delete(carModel);

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

        public async Task<List<CarModelDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarModel.Select()
                    .Include(x => x.CarBrand)
                    .Include(x => x.CarChassisType)
                    .Include(x => x.CarClass)
                    .Include(x => x.CarFuelType)
                    .Include(x => x.CarGearType)
                    .Include(x => x.CarType)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CarModelDTO>> GetByCarBrandIdAsync(int carBrandId)
        {
            try
            {
                return await unitOfWork.CarModel.Select(x => x.CarBrandId == carBrandId)
                    .Include(x => x.CarBrand)
                    .Include(x => x.CarChassisType)
                    .Include(x => x.CarClass)
                    .Include(x => x.CarFuelType)
                    .Include(x => x.CarGearType)
                    .Include(x => x.CarType)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarModelDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarModel.Select(x => x.Id == id)
                    .Include(x => x.CarBrand)
                    .Include(x => x.CarChassisType)
                    .Include(x => x.CarClass)
                    .Include(x => x.CarFuelType)
                    .Include(x => x.CarGearType)
                    .Include(x => x.CarType)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarModelDTO dto)
        {
            try
            {
                await isThereAnyCarModelValidation(dto);

                CarModel carModel = new CarModel()
                {
                    CarClassId = dto.CarClassId,
                    CarTypeId = dto.CarTypeId,
                    CarBrandId = dto.CarBrandId,
                    CarChassisTypeId = dto.CarChassisTypeId,
                    CarFuelTypeId = dto.CarFuelTypeId,
                    CarGearTypeId = dto.CarGearTypeId,
                    Name = dto.Name,
                    ImageUrl = dto.ImageUrl,
                    Description = dto.Description,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarModel.InsertAsync(carModel);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarModelDTO dto)
        {
            try
            {
                await isThereAnyCarModelValidation(dto);

                CarModel carModel = await getById(dto.Id);

                if (carModel == null)
                    throw new Exception("Kayıt bulunamadı.");

                carModel.CarClassId = dto.CarClassId;
                carModel.CarTypeId = dto.CarTypeId;
                carModel.CarBrandId = dto.CarBrandId;
                carModel.CarChassisTypeId = dto.CarChassisTypeId;
                carModel.CarFuelTypeId = dto.CarFuelTypeId;
                carModel.CarGearTypeId = dto.CarGearTypeId;
                carModel.Name = dto.Name;
                carModel.ImageUrl = dto.ImageUrl;
                carModel.Description = dto.Description;
                carModel.UpdateDate = DateTime.Now;

                unitOfWork.CarModel.Update(carModel);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarModel> getById(int id)
        {
            try
            {
                return await unitOfWork.CarModel.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task nameValidation(int carBrandId, string name)
        {
            try
            {
                ICarModelVL carClassVL = new CarModelVL();

                await carClassVL.IsThereName(carBrandId, name);
            }
            catch
            {
                throw;
            }
        }

        private async Task isThereAnyCarModelValidation(CarModelDTO dto)
        {
            try
            {
                ICarModelVL carModelVL = new CarModelVL();

                await carModelVL.IsThereAnyCarModel(dto);
            }
            catch
            {
                throw;
            }
        }
    }
}
