using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class MostPreferedCarViewModel
    {
        public string CarBrandName { get; set; }
        public string CarModelName { get; set; }
        public decimal CarRentalPrice { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
