using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class CarTypeDTO
    {
        public CarTypeDTO()
        {
            Car = new List<CarDTO>();
            CarModel = new List<CarModelDTO>();
        }

        public int Id { get; set; }

        [DisplayName("Araç Tipi :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<CarDTO> Car { get; set; }
        public List<CarModelDTO> CarModel { get; set; }
    }
}
