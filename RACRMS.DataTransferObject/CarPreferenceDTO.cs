using RACRMS.DataTransferObject.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RACRMS.DataTransferObject
{
    public partial class CarPreferenceDTO
    {
        public CarPreferenceDTO()
        {
            Cars = new List<CarDTO>();
        }

        public int Id { get; set; }
        [CustomRequired(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int CarId { get; set; }

        [DisplayName("Plakası :")]
        public string PlateNumber { get; set; }

        [DisplayName("Özellikler :")]
        public int PreferenceId { get; set; }

        [DisplayName("Özellik :")]
        public string PreferenceName { get; set; }

        public CarDTO Car { get; set; }
        public PreferenceDTO Preference { get; set; }

        public List<CarDTO> Cars { get; set; }

        [CustomArrayRequired(ErrorMessage = "En az bir özellik seçilmelidir.")]
        public PreferenceDTO[] Preferences { get; set; }
    }
}
