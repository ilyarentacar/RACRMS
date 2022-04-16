using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class CarViewModel
    {
        public CarViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false, bool EnableCarModelSelect = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;
            this.EnableCarModelSelect = EnableCarModelSelect;

            Cars = new List<CarDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; }
        public bool EnableCarModelSelect { get; }

        public CarDTO Car { get; set; }

        public List<CarDTO> Cars { get; set; }
    }
}
