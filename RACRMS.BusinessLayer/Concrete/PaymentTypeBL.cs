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
    public class PaymentTypeBL : IPaymentTypeBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public PaymentTypeBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                PaymentType paymentType = await getById(id);

                if (paymentType != null)
                {
                    unitOfWork.PaymentType.Delete(paymentType);

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

        public async Task<List<PaymentTypeDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.PaymentType.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaymentTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.PaymentType.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(PaymentTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                PaymentType paymentType = new PaymentType()
                {
                    Name = dto.Name,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.PaymentType.InsertAsync(paymentType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(PaymentTypeDTO dto)
        {
            try
            {
                await nameValidation(dto.Name);

                PaymentType paymentType = await getById(dto.Id);

                if (paymentType == null)
                    throw new Exception("Kayıt bulunamadı.");

                paymentType.Name = dto.Name;
                paymentType.UpdateDate = DateTime.Now;

                unitOfWork.PaymentType.Update(paymentType);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<PaymentType> getById(int id)
        {
            try
            {
                return await unitOfWork.PaymentType.Select(x => x.Id == id).FirstOrDefaultAsync();
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
                IPaymentTypeVL paymentTypeVL = new PaymentTypeVL();

                await paymentTypeVL.IsThereName(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
