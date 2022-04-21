using Microsoft.EntityFrameworkCore;
using RACRMS.DataAccessLayer.Abstract;
using RACRMS.Entity;
using RACRMS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.DataAccessLayer.Concrete
{
    public class PaymentTypeDAL : BaseRepository<PaymentType>, IPaymentTypeDAL
    {
        public PaymentTypeDAL(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
