using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface IReservationBL
    {
        Task<List<ReservationDTO>> GetAsync();
        Task<int> GetWaitingReservationCountAsync();
        Task<ReservationDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(ReservationDTO dto);
        Task<int> UpdateAsync(ReservationDTO dto);
        Task<int> UpdateAfterSentEmailAsync(ReservationDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
