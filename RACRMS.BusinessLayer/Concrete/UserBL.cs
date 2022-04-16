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
    public class UserBL : IUserBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public UserBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<UserDTO> CheckCridentialAsync(UserDTO dto)
        {
            try
            {
                UserDTO user = await unitOfWork.User
                    .Select()
                    .Include(x => x.UserRole)
                    .Where(x => x.Username == dto.Username)
                    .Where(x => x.Password == dto.Password)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();

                if (user == null)
                    throw new Exception("Kullanıcı Adı veya Şifre yanlış.");

                return user;

            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> CheckCridentialForPasswordRover(UserDTO dto)
        {
            try
            {
                UserDTO user = await unitOfWork.User
                    .Select()
                    .Where(x => x.Name == dto.Name)
                    .Where(x => x.Surname == dto.Surname)
                    .Where(x => x.EmailAddress == dto.EmailAddress)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();

                if (user == null)
                    throw new Exception("Kimlik bilgilerinize ait bir hesap bulunamadı.");

                return user;

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
                User user = await getById(id);

                if (user != null)
                {
                    unitOfWork.User.Delete(user);

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

        public async Task<List<UserDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.User.Select()
                    .Include(x => x.UserRole)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.User.Select(x => x.Id == id)
                    .Include(x => x.UserRole)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(UserDTO dto)
        {
            try
            {
                await usernameValidation(dto.Username);

                User user = new User()
                {
                    UserRoleId = dto.UserRoleId,
                    Name = dto.Name,
                    Surname = dto.Surname,
                    EmailAddress = dto.EmailAddress,
                    Username = dto.Username,
                    Password = dto.Password,
                    Editable = true,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.User.InsertAsync(user);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(UserDTO dto)
        {
            try
            {
                await isThereAnyUserValidation(dto);

                User user = await getById(dto.Id);

                if (user == null)
                    throw new Exception("Kayıt bulunamadı.");

                user.UserRoleId = dto.UserRoleId;
                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.EmailAddress = dto.EmailAddress;
                user.Username = dto.Username;
                user.Password = dto.Password;
                user.UpdateDate = DateTime.Now;

                unitOfWork.User.Update(user);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<User> getById(int id)
        {
            try
            {
                return await unitOfWork.User.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task usernameValidation(string username)
        {
            try
            {
                IUserVL userVL = new UserVL();

                await userVL.IsThereUsername(username);
            }
            catch
            {
                throw;
            }
        }

        private async Task isThereAnyUserValidation(UserDTO dto)
        {
            try
            {
                IUserVL userVL = new UserVL();

                await userVL.IsThereAnyUser(dto);
            }
            catch
            {
                throw;
            }
        }
    }
}
