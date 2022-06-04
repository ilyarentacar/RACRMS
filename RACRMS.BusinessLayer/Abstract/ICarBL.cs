using RACRMS.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.BusinessLayer.Abstract
{
    public interface ICarBL
    {
        Task<List<CarDTO>> GetAsync();
        Task<CarDTO> GetByIdAsync(int id);
        Task<int> InsertAsync(CarDTO dto);
        Task<int> UpdateAsync(CarDTO dto);
        Task<int> DeleteAsync(int id);
        Task<CarDTO> GetMostPrefered();
        Task<List<CarDTO>> GetFilo();
        Task<List<CarDTO>> GetFiloByReservation(DateTime startDate, DateTime endDate);
        Task<List<CarDTO>> GetEconomicFilo();
        Task<List<CarDTO>> GetEconomicFiloByReservation(DateTime startDate, DateTime endDate);
        Task<List<CarDTO>> GetConfortFilo();
        Task<List<CarDTO>> GetConfortFiloByReservation(DateTime startDate, DateTime endDate);
        Task<List<CarDTO>> GetLuxFilo();
        Task<List<CarDTO>> GetLuxFiloByReservation(DateTime startDate, DateTime endDate);
    }
}
