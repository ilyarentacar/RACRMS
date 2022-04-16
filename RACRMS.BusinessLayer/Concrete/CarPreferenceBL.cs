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
    public class CarPreferenceBL : ICarPreferenceBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarPreferenceBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task BulkInsertAsync(CarPreferenceDTO dto)
        {
            try
            {
                List<CarPreference> carPreferences = dto.Preferences.Where(x => x.Selected).Select(x => new CarPreference()
                {
                    CarId = dto.CarId,
                    PreferenceId = x.Id
                }).ToList();

                await unitOfWork.CarPreference.InsertRangeAsync(carPreferences);

                await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                CarPreference carPreference = await getById(id);

                if (carPreference != null)
                {
                    unitOfWork.CarPreference.Delete(carPreference);

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

        public async Task<List<CarPreferenceDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarPreference.Select()
                    .Include(x => x.Car)
                    .Include(x => x.Preference)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarPreferenceDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarPreference.Select(x => x.Id == id)
                    .Include(x => x.Car)
                    .Include(x => x.Preference)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarPreferenceDTO dto)
        {
            try
            {
                await preferenceValidation(dto.CarId, dto.PreferenceId);

                CarPreference carPreference = new CarPreference()
                {
                    CarId = dto.CarId,
                    PreferenceId = dto.PreferenceId
                };

                await unitOfWork.CarPreference.InsertAsync(carPreference);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarPreference> getById(int id)
        {
            try
            {
                return await unitOfWork.CarPreference.Select().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task preferenceValidation(int carId, int preferenceId)
        {
            try
            {
                ICarPreferenceVL carPreferenceVL = new CarPreferenceVL();

                await carPreferenceVL.IsTherePreference(carId, preferenceId);
            }
            catch
            {
                throw;
            }
        }
    }
}
