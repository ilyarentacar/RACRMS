using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RACRMS.DataTransferObject
{
    public partial class CarModelDTO
    {
        public CarModelDTO()
        {
            Car = new List<CarDTO>();
            CarBrands = new List<CarBrandDTO>();
            CarClasses = new List<CarClassDTO>();
            CarTypes = new List<CarTypeDTO>();
            CarChassisTypes = new List<CarChassisTypeDTO>();
            CarFuelTypes = new List<CarFuelTypeDTO>();
            CarGearTypes = new List<CarGearTypeDTO>();

            CarImages = new Dictionary<string, string>();

            CarImages.Add("Citroen C Elysee", "citroen-c-elysee.png");
            CarImages.Add("Dacia Sandero Stepway", "dacia-sandero-stepway.png");
            CarImages.Add("Fiat Egea", "fiat-egea.png");
            CarImages.Add("Ford Ecosport", "ford-ecosport.png");
            CarImages.Add("Ford Focus", "ford-focus.png");
            CarImages.Add("Hyundai Elantra", "hyundai-elantra.png");
            CarImages.Add("Hyundai i20", "hyundai-i20.png");
            CarImages.Add("Jeep Compass", "jeep-compass.png");
            CarImages.Add("Kia Cerato", "kia-cerato.png");
            CarImages.Add("Peugeot 208", "peugeot-208.png");
            CarImages.Add("Peugeot 301", "peugeot-301.png");
            CarImages.Add("Renault Clio", "renault-clio.png");
            CarImages.Add("Renault Express", "renault-express.png");
            CarImages.Add("Renault Fluence", "renault-fluence.png");
            CarImages.Add("Renault Megane", "renault-megane.png");
            CarImages.Add("Renault Taliant", "renault-taliant.png");
            CarImages.Add("Volkswagen Polo", "volkswagen-polo.png");
            CarImages.Add("Volkswagen T-Roc", "volkswagen-troc.png");
            CarImages.Add("Renault Capture", "renault-capture.png");

            CarImages = CarImages
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public int Id { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarClassId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarBrandId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarChassisTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarFuelTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarGearTypeId { get; set; }
        [DisplayName("Adı :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
        [DisplayName("Araç Resmi :")]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarBrandDTO CarBrand { get; set; }
        public CarChassisTypeDTO CarChassisType { get; set; }
        public CarClassDTO CarClass { get; set; }
        public CarFuelTypeDTO CarFuelType { get; set; }
        public CarGearTypeDTO CarGearType { get; set; }
        public CarTypeDTO CarType { get; set; }
        public List<CarDTO> Car { get; set; }

        [DisplayName("Araç Markası :")]
        public string CarBrandName { get; set; }
        [DisplayName("Araç Sınıfı :")]
        public string CarClassName { get; set; }
        [DisplayName("Araç Tipi :")]
        public string CarTypeName { get; set; }
        [DisplayName("Araç Kasa Tipi :")]
        public string CarChassisTypeName { get; set; }
        [DisplayName("Araç Yakıt Tipi :")]
        public string CarFuelTypeName { get; set; }
        [DisplayName("Araç Vites Tipi :")]
        public string CarGearTypeName { get; set; }

        public List<CarBrandDTO> CarBrands { get; set; }
        public List<CarClassDTO> CarClasses { get; set; }
        public List<CarTypeDTO> CarTypes { get; set; }
        public List<CarChassisTypeDTO> CarChassisTypes { get; set; }
        public List<CarFuelTypeDTO> CarFuelTypes { get; set; }
        public List<CarGearTypeDTO> CarGearTypes { get; set; }

        public Dictionary<string, string> CarImages { get; set; }
    }
}
