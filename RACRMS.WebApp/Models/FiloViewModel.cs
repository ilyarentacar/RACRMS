using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class FiloViewModel
    {
        public List<CarViewModel> carViewModels { get; set; }

        public class CarViewModel
        {
            public string ImageUrl { get; set; }
            public string CarClassName { get; set; }
            public string CarChassisTypeName { get; set; }
            public decimal CarRentalPrice { get; set; }
            public string CarBrandName { get; set; }
            public string CarModelName { get; set; }
            public string CarFuelTypeName { get; set; }
            public string CarGearTypeName { get; set; }
            public int TotalKm { get; set; }
            public string AgeLimit { get; set; }
        }
    }
}
