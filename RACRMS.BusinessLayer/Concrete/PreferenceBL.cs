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
  public  class PreferenceBL: IPreferenceBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public PreferenceBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Preference preference = await getById(id);

                if (preference != null)
                {
                    unitOfWork.Preference.Delete(preference);

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

        public async Task<List<PreferenceDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Preference.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<PreferenceDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Preference.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(PreferenceDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                Preference preference = new Preference()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.Preference.InsertAsync(preference);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(PreferenceDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                Preference preference = await getById(dto.Id);

                if (preference == null)
                    throw new Exception("Kayıt bulunamadı.");

                preference.Name = dto.Name;
                preference.UpdateDate = DateTime.Now;

                unitOfWork.Preference.Update(preference);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Preference> getById(int id)
        {
            try
            {
                return await unitOfWork.Preference.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                IPreferenceVL preferenceVL = new PreferenceVL();

                await preferenceVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
