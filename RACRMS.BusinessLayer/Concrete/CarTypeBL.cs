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
public    class CarTypeBL: ICarTypeBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarTypeBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarType carType = await getById(id);

                if (carType != null)
                {
                    unitOfWork.CarType.Delete(carType);

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

        public async Task<List<CarTypeDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarType.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarType.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarType carType = new CarType()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarType.InsertAsync(carType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarType carType = await getById(dto.Id);

                if (carType == null)
                    throw new Exception("Kayıt bulunamadı.");

                carType.Name = dto.Name;
                carType.UpdateDate = DateTime.Now;

                unitOfWork.CarType.Update(carType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarType> getById(int id)
        {
            try
            {
                return await unitOfWork.CarType.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                ICarTypeVL carClassVL = new CarTypeVL();

                await carClassVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
