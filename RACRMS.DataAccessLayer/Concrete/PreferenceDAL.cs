using Microsoft.EntityFrameworkCore;
using RACRMS.DataAccessLayer.Abstract;
using RACRMS.Entity;
using RACRMS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RACRMS.DataAccessLayer.Concrete
{
    public class PreferenceDAL : BaseRepository<Preference>, IPreferenceDAL
    {
        public PreferenceDAL(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
