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
    public class CarFuelTypeBL : ICarFuelTypeBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarFuelTypeBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarFuelType carFuelType = await getById(id);

                if (carFuelType != null)
                {
                    unitOfWork.CarFuelType.Delete(carFuelType);

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

        public async Task<List<CarFuelTypeDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarFuelType.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarFuelTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarFuelType.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarFuelTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarFuelType carFuelType = new CarFuelType()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarFuelType.InsertAsync(carFuelType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarFuelTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarFuelType carFuelType = await getById(dto.Id);

                if (carFuelType == null)
                    throw new Exception("Kayıt bulunamadı.");

                carFuelType.Name = dto.Name;
                carFuelType.UpdateDate = DateTime.Now;

                unitOfWork.CarFuelType.Update(carFuelType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarFuelType> getById(int id)
        {
            try
            {
                return await unitOfWork.CarFuelType.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task nameValidation(string name)
        {
            try
            {
                ICarFuelTypeVL carClassVL = new CarFuelTypeVL();

                await carClassVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
