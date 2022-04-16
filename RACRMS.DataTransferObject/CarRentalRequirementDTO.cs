using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RACRMS.DataTransferObject
{
    public partial class CarRentalRequirementDTO
    {
        public CarRentalRequirementDTO()
        {
            Cars = new List<CarDTO>();
        }

        public int Id { get; set; }
        public int CarId { get; set; }
        [DisplayName("Plakası :")]
        public string PlateNumber { get; set; }
        [DisplayName("Şartlar :")]
        public int RequirementId { get; set; }
        [DisplayName("Şart :")]
        public string RequirementName { get; set; }

        public CarDTO Car { get; set; }
        public RequirementDTO Requirement { get; set; }        

        public List<CarDTO> Cars { get; set; }
        public RequirementDTO[] Requirements { get; set; }
    }
}
