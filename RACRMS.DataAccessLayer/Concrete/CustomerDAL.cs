using Microsoft.EntityFrameworkCore;
using RACRMS.DataAccessLayer.Abstract;
using RACRMS.Entity;
using RACRMS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.DataAccessLayer.Concrete
{
    public class CustomerDAL : BaseRepository<Customer>, ICustomerDAL
    {
        public CustomerDAL(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
