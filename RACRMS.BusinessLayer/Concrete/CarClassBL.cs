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
    public class CarClassBL : ICarClassBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarClassBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarClass carClass = await getById(id);

                if (carClass != null)
                {
                    unitOfWork.CarClass.Delete(carClass);

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

        public async Task<List<CarClassDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarClass.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarClassDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarClass.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarClassDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarClass carClass = new CarClass()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarClass.InsertAsync(carClass);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarClassDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarClass carClass = await getById(dto.Id);

                if (carClass == null)
                    throw new Exception("Kayıt bulunamadı.");

                carClass.Name = dto.Name;
                carClass.UpdateDate = DateTime.Now;

                unitOfWork.CarClass.Update(carClass);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarClass> getById(int id)
        {
            try
            {
                return await unitOfWork.CarClass.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                ICarClassVL carClassVL = new CarClassVL();

                await carClassVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
