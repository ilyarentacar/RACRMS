using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class CarRentalPriceDTO
    {
        public CarRentalPriceDTO()
        {
            Cars = new List<CarDTO>();
        }

        public int Id { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarId { get; set; }

        [DisplayName("Plakası :")]
        public string PlateNumber { get; set; }

        [DisplayName("Başlangıç Tarihi :")]
        public string StartDateStr { get; set; }
        public DateTime StartDate { get; set; }

        [DisplayName("Bitiş Tarihi :")]
        public string EndDateStr { get; set; }
        public DateTime EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DisplayName("Kiralama Ücreti :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public decimal RentPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CarDTO Car { get; set; }

        public List<CarDTO> Cars { get; set; }
    }
}
