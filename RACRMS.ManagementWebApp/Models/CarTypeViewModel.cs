using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class CarTypeViewModel
    {
        public CarTypeViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;

            CarTypes = new List<CarTypeDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; set; }

        public CarTypeDTO CarType { get; set; }

        public List<CarTypeDTO> CarTypes { get; set; }
    }
}
