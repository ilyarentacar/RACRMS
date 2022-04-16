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
    public class CustomerBL : ICustomerBL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CustomerBL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Customer customer = await getById(id);

                if (customer != null)
                {
                    unitOfWork.Customer.Delete(customer);

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

        public async Task<List<CustomerDTO>> GetAsync()
        {
            try
            {
                return await unitOfWork.Customer.Select()
                    .Select(x => x.ToDTO()).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            try
            {
                return await unitOfWork.Customer.Select(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(CustomerDTO dto)
        {
            try
            {
                await identityNumberValidation(dto.IdentityNumber);

                Customer customer = new Customer()
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    IdentityNumber = dto.IdentityNumber,
                    EmailAddress = dto.EmailAddress,
                    CellNumber = dto.CellNumber,
                    CreateDate = DateTime.Now
                };

                await unitOfWork.Customer.InsertAsync(customer);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(CustomerDTO dto)
        {
            try
            {
                await isThereAnyCustomerValidation(dto);

                Customer customer = await getById(dto.Id);

                if (customer == null)
                    throw new Exception("Kayıt bulunamadı.");

                customer.Name = dto.Name;
                customer.Surname = dto.Surname;
                customer.IdentityNumber = dto.IdentityNumber;
                customer.EmailAddress = dto.EmailAddress;
                customer.CellNumber = dto.CellNumber;
                customer.UpdateDate = DateTime.Now;

                unitOfWork.Customer.Update(customer);

                return await unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<Customer> getById(int id)
        {
            try
            {
                return await unitOfWork.Customer.Select(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task identityNumberValidation(decimal identityNumber)
        {
            try
            {
                ICustomerVL customerVL = new CustomerVL();

                customerVL.IdentityNumverValid(identityNumber);

                await customerVL.IsThereIdentityNumber(identityNumber);
            }
            catch
            {
                throw;
            }
        }

        private async Task isThereAnyCustomerValidation(CustomerDTO dto)
        {
            try
            {
                ICustomerVL customerVL = new CustomerVL();

                customerVL.IdentityNumverValid(dto.IdentityNumber);

                await customerVL.IsThereAnyCustomer(dto);
            }
            catch
            {
                throw;
            }
        }
    }
}
