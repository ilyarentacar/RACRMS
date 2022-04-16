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
    public class UserRoleBL : IUserRoleBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public UserRoleBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                UserRole userRole = await getById(id);

                if (userRole != null)
                {
                    unitOfWork.UserRole.Delete(userRole);

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

        public async Task<List<UserRoleDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.UserRole
                    .Select(x => x.Usable)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserRoleDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.UserRole.Select(x => x.Id == id).Where(x => x.Usable).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(UserRoleDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                UserRole userRole = new UserRole()
                {
                    Name = dto.Name,
                    Usable = true,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.UserRole.InsertAsync(userRole);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(UserRoleDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                UserRole userRole = await getById(dto.Id);

                if (userRole == null)
                    throw new Exception("Kayıt bulunamadı.");

                userRole.Name = dto.Name;
                userRole.UpdateDate = DateTime.Now;

                unitOfWork.UserRole.Update(userRole);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<UserRole> getById(int id)
        {
            try
            {
                return await unitOfWork.UserRole.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                IUserRoleVL userRoleVL = new UserRoleVL();

                await userRoleVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
