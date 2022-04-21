using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class CarDTO
    {
        public CarDTO()
        {
            CarPreference = new List<CarPreferenceDTO>();
            CarRentalPrice = new List<CarRentalPriceDTO>();
            CarRentalRequirement = new List<CarRentalRequirementDTO>();
            Contract = new HashSet<ContractDTO>();
            Reservation = new List<ReservationDTO>();
            CarBrands = new List<CarBrandDTO>();
            CarModels = new List<CarModelDTO>();
        }

        public int Id { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarClassId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarBrandId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarModelId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarChassisTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarFuelTypeId { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarGearTypeId { get; set; }

        [DisplayName("Kiraya Gidebilir")]
        public bool Rentable { get; set; }

        [DisplayName("Plakası :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(10)]
        public string PlateNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarBrandDTO CarBrand { get; set; }
        public CarChassisTypeDTO CarChassisType { get; set; }
        public CarClassDTO CarClass { get; set; }
        public CarFuelTypeDTO CarFuelType { get; set; }
        public CarGearTypeDTO CarGearType { get; set; }
        public CarModelDTO CarModel { get; set; }
        public CarTypeDTO CarType { get; set; }
        public List<CarPreferenceDTO> CarPreference { get; set; }
        public List<CarRentalPriceDTO> CarRentalPrice { get; set; }
        public List<CarRentalRequirementDTO> CarRentalRequirement { get; set; }
        public ICollection<ContractDTO> Contract { get; set; }
        public List<ReservationDTO> Reservation { get; set; }

        [DisplayName("Araç Markası :")]
        public string CarBrandName { get; set; }
        [DisplayName("Araç Modeli :")]
        public string CarModelName { get; set; }
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
        public List<CarModelDTO> CarModels { get; set; }
    }
}
