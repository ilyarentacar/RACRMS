using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class CarRentalPriceViewModel
    {
        public CarRentalPriceViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;

            CarRentalPrices = new List<CarRentalPriceDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; set; }

        public CarRentalPriceDTO CarRentalPrice { get; set; }

        public List<CarRentalPriceDTO> CarRentalPrices { get; set; }
    }
}
