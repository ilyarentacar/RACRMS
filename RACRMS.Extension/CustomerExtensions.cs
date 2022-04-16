using RACRMS.DataTransferObject;
using RACRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.Extension
{
    public static class CustomerExtensions
    {
        public static CustomerDTO ToDTO(this Customer entity)
        {
            try
            {
                return new CustomerDTO()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    IdentityNumber = entity.IdentityNumber,
                    EmailAddress = entity.EmailAddress,
                    CellNumber = entity.CellNumber,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
