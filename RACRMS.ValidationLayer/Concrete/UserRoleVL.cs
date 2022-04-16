using Microsoft.EntityFrameworkCore;
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
    public class UserRoleVL: IUserRoleVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public UserRoleVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereName(string name)
        {
            try
            {
                if (await unitOfWork.UserRole.Select(x => x.Name == name).AnyAsync())
                    throw new Exception("Bu özellik zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
