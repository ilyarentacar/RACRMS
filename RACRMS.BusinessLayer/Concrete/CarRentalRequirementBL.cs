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
    public class CarRentalRequirementBL : ICarRentalRequirementBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CarRentalRequirementBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task BulkInsertAsync(CarRentalRequirementDTO dto)
        {
            try
            {
                List<CarRentalRequirement> carPreferences = dto.Requirements.Where(x => x.Selected).Select(x => new CarRentalRequirement()
                {
                    CarId = dto.CarId,
                    RequirementId = x.Id
                }).ToList();

                await unitOfWork.CarRentalRequirement.InsertRangeAsync(carPreferences);

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
                CarRentalRequirement carRentalRequirement = await getById(id);

                if (carRentalRequirement != null)
                {
                    unitOfWork.CarRentalRequirement.Delete(carRentalRequirement);

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

        public async Task<List<CarRentalRequirementDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.CarRentalRequirement.Select()
                    .Include(x => x.Car)
                    .Include(x => x.Requirement)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CarRentalRequirementDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.CarRentalRequirement.Select(x => x.Id == id)
                    .Include(x => x.Car)
                    .Include(x => x.Requirement)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CarRentalRequirementDTO dto)
        {
            try
            {
                await requirementValidation(dto.CarId, dto.RequirementId);

                CarRentalRequirement carRentalRequirement = new CarRentalRequirement()
                {
                    CarId = dto.CarId,
                    RequirementId = dto.RequirementId
                };

                await unitOfWork.CarRentalRequirement.InsertAsync(carRentalRequirement);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<CarRentalRequirement> getById(int id)
        {
            try
            {
                return await unitOfWork.CarRentalRequirement.Select().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task requirementValidation(int carId, int requirementId)
        {
            try
            {
                ICarRentalRequirementVL carRentalRequirementVL = new CarRentalRequirementVL();

                await carRentalRequirementVL.IsThereRequirement(carId, requirementId);
            }
            catch
            {
                throw;
            }
        }
    }
}
