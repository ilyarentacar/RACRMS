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
    public class CustomerVL : ICustomerVL
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public CustomerVL()
        {
            unitOfWork = new BaseUnitOfWork();
        }

        public async Task IsThereIdentityNumber(decimal identityNumber)
        {
            try
            {
                if (await unitOfWork.Customer.Select(x => x.IdentityNumber == identityNumber).AnyAsync())
                    throw new Exception("Bu kimlik numarasına sahip müşteri zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }

        public void IdentityNumverValid(decimal identityNumber)
        {
            try
            {
                bool result = false;

                if (identityNumber.ToString().Length == 11)
                {
                    Int64 ATCNO, BTCNO, TcNo;
                    long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                    TcNo = Int64.Parse(identityNumber.ToString());

                    ATCNO = TcNo / 100;
                    BTCNO = TcNo / 100;

                    C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                    Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                    Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                    result = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
                }

                if (!result)
                    throw new Exception("Lütfen geçerli bir TC kimlik numarası giriniz.");
            }
            catch
            {
                throw;
            }
        }

        public async Task IsThereAnyCustomer(CustomerDTO dto)
        {
            try
            {
                if (await unitOfWork.Customer.Select()
                    .Where(x => x.Name == dto.Name)
                    .Where(x => x.Surname == dto.Surname)
                    .Where(x => x.IdentityNumber == dto.IdentityNumber)
                    .Where(x => x.EmailAddress == dto.EmailAddress)
                    .Where(x => x.CellNumber == dto.CellNumber)
                    .AnyAsync())
                    throw new Exception("Bu müşteri zaten kayıtlıdır.");
            }
            catch
            {
                throw;
            }
        }
    }
}
