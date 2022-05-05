using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class FiloViewModel
    {
        public FiloViewModel()
        {
            carViewModels = new List<CarViewModel>()
            {
                new CarViewModel()
                {
                    CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/o-renault-megane.png",
                    CarClassName = "Ekonomi",
                    CarChassisTypeName = "Sedan",
                    CarRentalPrice = 600,
                    CarBrandName = "Renault",
                    CarModelName = "Megane 1.5 DCI",
                    CarFuelTypeName = "Dizel",
                    CarGearTypeName = "Otomatik",
                    TotalKm = 500,
                    AgeLimit = "21 Yaş ve üzeri"
                },
                new CarViewModel()
                {
                    CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/b-fiat-egea-cross.png",
                    CarClassName = "Lüx",
                    CarChassisTypeName = "SUV",
                    CarRentalPrice = 500,
                    CarBrandName = "Fiat",
                    CarModelName = "Egea Cross",
                    CarFuelTypeName = "Dizel",
                    CarGearTypeName = "Otomatik",
                    TotalKm = 500,
                    AgeLimit = "21 Yaş ve üzeri"
                },
                new CarViewModel()
                {
                    CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/n-fiat-egea.png",
                    CarClassName = "Ekonomi",
                    CarChassisTypeName = "Sedan",
                    CarRentalPrice = 500,
                    CarBrandName = "Fiat",
                    CarModelName = "Egea 1.3 Multijet",
                    CarFuelTypeName = "Dizel",
                    CarGearTypeName = "Otomatik",
                    TotalKm = 500,
                    AgeLimit = "21 Yaş ve üzeri"
                },
                new CarViewModel()
                {
                    CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/h-bmw-2-serisi.png",
                    CarClassName = "Lüx",
                    CarChassisTypeName = "Sedan",
                    CarRentalPrice = 500,
                    CarBrandName = "Bmw",
                    CarModelName = "2",
                    CarFuelTypeName = "Benzin",
                    CarGearTypeName = "Otomatik",
                    TotalKm = 500,
                    AgeLimit = "21 Yaş ve üzeri"
                },
                new CarViewModel()
                {
                    CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/h-bmw-2-serisi.png",
                    CarClassName = "Lüx",
                    CarChassisTypeName = "Sedan",
                    CarRentalPrice = 500,
                    CarBrandName = "Bmw",
                    CarModelName = "2",
                    CarFuelTypeName = "Benzin",
                    CarGearTypeName = "Otomatik",
                    TotalKm = 500,
                    AgeLimit = "21 Yaş ve üzeri"
                }
            };
        }

        public List<CarViewModel> carViewModels { get; set; }

        public class CarViewModel
        {
            public string CarImage { get; set; }
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
