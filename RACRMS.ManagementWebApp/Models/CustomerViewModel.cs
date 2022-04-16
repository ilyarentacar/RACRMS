using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;

            Customers = new List<CustomerDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; set; }

        public CustomerDTO Customer { get; set; }

        public List<CustomerDTO> Customers { get; set; }
    }
}
