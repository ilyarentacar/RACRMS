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
    public class CarBrandBL : ICarBrandBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarBrandBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarBrand carBrand = await getById(id);

                if (carBrand != null)
                {
                    unitOfWork.CarBrand.Delete(carBrand);

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

        public async Task<List<CarBrandDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarBrand.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarBrandDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarBrand.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarBrandDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarBrand carBrand = new CarBrand()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarBrand.InsertAsync(carBrand);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarBrandDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarBrand carBrand = await getById(dto.Id);

                if (carBrand == null)
                    throw new Exception("Kayıt bulunamadı.");

                carBrand.Name = dto.Name;
                carBrand.UpdateDate = DateTime.Now;

                unitOfWork.CarBrand.Update(carBrand);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarBrand> getById(int id)
        {
            try
            {
                return await unitOfWork.CarBrand.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                ICarBrandVL carBrandVL = new CarBrandVL();

                await carBrandVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
