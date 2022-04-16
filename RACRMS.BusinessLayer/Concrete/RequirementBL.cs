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
    public class RequirementBL : IRequirementBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public RequirementBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Requirement requirement = await getById(id);

                if (requirement != null)
                {
                    unitOfWork.Requirement.Delete(requirement);

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

        public async Task<List<RequirementDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Requirement.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<RequirementDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Requirement.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(RequirementDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                Requirement requirement = new Requirement()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.Requirement.InsertAsync(requirement);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(RequirementDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                Requirement requirement = await getById(dto.Id);

                if (requirement == null)
                    throw new Exception("Kayıt bulunamadı.");

                requirement.Name = dto.Name;
                requirement.UpdateDate = DateTime.Now;

                unitOfWork.Requirement.Update(requirement);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Requirement> getById(int id)
        {
            try
            {
                return await unitOfWork.Requirement.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                IRequirementVL requirementVL = new RequirementVL();

                await requirementVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
