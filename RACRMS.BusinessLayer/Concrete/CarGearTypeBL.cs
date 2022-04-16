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
    public class CarGearTypeBL : ICarGearTypeBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarGearTypeBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarGearType carGearType = await getById(id);

                if (carGearType != null)
                {
                    unitOfWork.CarGearType.Delete(carGearType);

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

        public async Task<List<CarGearTypeDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarGearType.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarGearTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarGearType.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarGearTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarGearType carGearType = new CarGearType()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarGearType.InsertAsync(carGearType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarGearTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarGearType carGearType = await getById(dto.Id);

                if (carGearType == null)
                    throw new Exception("Kayıt bulunamadı.");

                carGearType.Name = dto.Name;
                carGearType.UpdateDate = DateTime.Now;

                unitOfWork.CarGearType.Update(carGearType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarGearType> getById(int id)
        {
            try
            {
                return await unitOfWork.CarGearType.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                ICarGearTypeVL carClassVL = new CarGearTypeVL();

                await carClassVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
