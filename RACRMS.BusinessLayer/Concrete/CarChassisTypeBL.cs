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
    public class CarChassisTypeBL : ICarChassisTypeBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarChassisTypeBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarChassisType carChassisType = await getById(id);

                if (carChassisType != null)
                {
                    unitOfWork.CarChassisType.Delete(carChassisType);

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

        public async Task<List<CarChassisTypeDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarChassisType.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarChassisTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarChassisType.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarChassisTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarChassisType carChassisType = new CarChassisType()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.CarChassisType.InsertAsync(carChassisType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CarChassisTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                CarChassisType carChassisType = await getById(dto.Id);

                if (carChassisType == null)
                    throw new Exception("Kayıt bulunamadı.");

                carChassisType.Name = dto.Name;
                carChassisType.UpdateDate = DateTime.Now;

                unitOfWork.CarChassisType.Update(carChassisType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarChassisType> getById(int id)
        {
            try
            {
                return await unitOfWork.CarChassisType.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                ICarChassisTypeVL carChassisTypeVL = new CarChassisTypeVL();

                await carChassisTypeVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
