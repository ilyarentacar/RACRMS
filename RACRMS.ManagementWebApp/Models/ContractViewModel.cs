using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Models
{
    public class ContractViewModel
    {
        public ContractViewModel(bool OpenInsertPopup = false, bool OpenUpdatePopup = false, bool OpenDeletePopup = false, bool OpenDetailPopup = false)
        {
            this.OpenInsertPopup = OpenInsertPopup;
            this.OpenUpdatePopup = OpenUpdatePopup;
            this.OpenDeletePopup = OpenDeletePopup;
            this.OpenDetailPopup = OpenDetailPopup;

            Contracts = new List<ContractDTO>();
        }

        public bool OpenInsertPopup { get; }
        public bool OpenUpdatePopup { get; }
        public bool OpenDeletePopup { get; set; }
        public bool OpenDetailPopup { get; }

        public ContractDTO Contract { get; set; }

        public List<ContractDTO> Contracts { get; set; }
    }
}
