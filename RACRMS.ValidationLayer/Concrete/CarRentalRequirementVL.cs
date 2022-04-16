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
    public class CarRentalRequirementVL : ICarRentalRequirementVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarRentalRequirementVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereRequirement(int carId, int requirementId)
        {
            try
            {
                if (await unitOfWork.CarRentalRequirement.Select().Where(x => x.CarId == carId).Where(x => x.RequirementId == requirementId).AnyAsync())
                    throw new Exception("Bu kiralama şartı zaten eklenmiştir.");
            }
            catch
            {
                throw;
            }
        }
    }
}
