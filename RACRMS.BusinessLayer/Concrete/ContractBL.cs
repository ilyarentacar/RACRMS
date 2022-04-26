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
    public class ContractBL : IContractBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public ContractBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Contract contract = await getById(id);

                if (contract != null)
                {
                    unitOfWork.Contract.Delete(contract);

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

        public async Task<List<ContractDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Contract.Select()
                    .Include(x => x.Reservation)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarBrand)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarModel)
                    .Include(x => x.Customer)
                    .Include(x => x.PaymentType)
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ContractDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Contract.Select(x => x.Id == id)
                    .Include(x => x.Reservation)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarBrand)
                    .Include(x => x.Car)
                        .ThenInclude(x => x.CarModel)
                    .Include(x => x.Customer)
                    .Include(x => x.PaymentType)
                    .Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(ContractDTO dto)
        {
            try
            {
                await dateValidation(dto.StartDate, dto.PlanedEndDate);

                Contract contract = new Contract()
                {
                    ReservationId = dto.ReservationId,
                    CarId = dto.CarId,
                    CustomerId = dto.CustomerId,
                    StartDate = dto.StartDate,
                    PlanedEndDate = dto.PlanedEndDate,
                    EndDate = dto.EndDate,
                    ContractCompleted = dto.ContractCompleted,
                    ContractCompletedDate = dto.ContractCompletedDate,
                    TotalPrice = dto.TotalPrice,
                    HasPaid = dto.HasPaid,
                    PaymentTypeId = dto.PaymentTypeId,
                    CardOwnerName = dto.CardOwnerName,
                    CardNumber = dto.CardNumber,
                    CardValidDate = dto.CardValidDate,
                    CardCvv = dto.CardCvv,
                    CreateDate = dto.CreateDate
                };

                await unitOfWork.Contract.InsertAsync(contract);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(ContractDTO dto)
        {
            try
            {
                await dateValidation(dto.StartDate, dto.PlanedEndDate);

                Contract contract = await getById(dto.Id);

                if (contract == null)
                    throw new Exception("Kayıt bulunamadı.");

                contract.StartDate = dto.StartDate;
                contract.PlanedEndDate = dto.PlanedEndDate;
                contract.EndDate = dto.EndDate;
                contract.TotalPrice = dto.TotalPrice;
                contract.HasPaid = dto.HasPaid;
                contract.PaymentTypeId = dto.PaymentTypeId;
                contract.CardOwnerName = dto.CardOwnerName;
                contract.CardNumber = dto.CardNumber;
                contract.CardValidDate = dto.CardValidDate;
                contract.CardCvv = dto.CardCvv;
                contract.UpdateDate = DateTime.Now;

                unitOfWork.Contract.Update(contract);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Contract> getById(int id)
        {
            try
            {
                return await unitOfWork.Contract.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task dateValidation(DateTime startDate, DateTime endDate)
        {
            try
            {
                IContractVL contractVL = new ContractVL();

                await contractVL.AreContractDatesAvailable(startDate, endDate);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetWaitingContractCountAsync()
        {
            try
            {
                return await unitOfWork.Contract.Select().Where(x => !x.HasPaid).Where(x => !x.ContractCompleted.HasValue).CountAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAfterSentEmailAsync(ContractDTO dto)
        {
            try
            {
                Contract contract = await getById(dto.Id);

                if (contract == null)
                    throw new Exception("Kayıt bulunamadı.");

                contract.EmailSent = dto.EmailSent;
                contract.EmailSentDate = dto.EmailSentDate;
                contract.EmailTemplate = dto.EmailTemplate;
                contract.UpdateDate = DateTime.Now;

                unitOfWork.Contract.Update(contract);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
