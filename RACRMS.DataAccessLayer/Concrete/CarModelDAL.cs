using Microsoft.EntityFrameworkCore;
using RACRMS.DataAccessLayer.Abstract;
using RACRMS.Entity;
using RACRMS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.DataAccessLayer.Concrete
{
    public class CarModelDAL : BaseRepository<CarModel>, ICarModelDAL
    {
        public CarModelDAL(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
