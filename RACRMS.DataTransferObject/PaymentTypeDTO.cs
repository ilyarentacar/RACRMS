using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.DataTransferObject
{
    public partial class PaymentTypeDTO
    {
        public PaymentTypeDTO()
        {
            Contract = new HashSet<ContractDTO>();
        }

        public int Id { get; set; }
        [DisplayName("Özellik :")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [MaxLength(15)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<ContractDTO> Contract { get; set; }
    }
}
