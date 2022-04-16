using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarRentalRequirementBL
    {
        Task<List<CarRentalRequirementDTO>> GetAsync();
        Task<CarRentalRequirementDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarRentalRequirementDTO dto);
        Task BulkInsertAsync(CarRentalRequirementDTO dto);
        Task<int> DeleteAsync(int id);
    }
}
