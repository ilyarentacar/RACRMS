using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RACRMS.DataTransferObject
{
    public partial class RequirementDTO
    {
        public RequirementDTO()
        {
            CarRentalRequirement = new List<CarRentalRequirementDTO>();
        }

        public int Id { get; set; }

        [DisplayName("Şart :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool Selected { get; set; }

        public List<CarRentalRequirementDTO> CarRentalRequirement { get; set; }
    }
}
