using Microsoft.EntityFrameworkCore;
using RACRMS.DataTransferObject;
using RACRMS.UnitOfWork.Abstract;
using RACRMS.UnitOfWork.Concrete;
using RACRMS.ValidationLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.ValidationLayer.Concrete
{
    public class UserVL : IUserVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public UserVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereAnyUser(UserDTO dTO)
        {
            try
            {
                if (await unitOfWork.User.Select()
                    .Where(x => x.UserRoleId == dTO.UserRoleId)
                    .Where(x => x.Name == dTO.Name)
                    .Where(x => x.Surname == dTO.Surname)
                    .Where(x => x.EmailAddress == dTO.EmailAddress)
                    .Where(x => x.Username == dTO.Username)
                    .Where(x => x.Password == dTO.Password)
                    .AnyAsync())
                    throw new Exception("Bu Kullanıcı kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }

        public async Task IsThereUsername(string username)
        {
            try
            {
                if (await unitOfWork.User.Select(x => x.Username == username).AnyAsync())
                    throw new Exception("Bu Kullanıcı Adı kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
