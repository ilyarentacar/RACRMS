using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public Guid CarImageId { get; set; }
        [DisplayName("Adı :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
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
    }
}
