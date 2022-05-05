using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            reservationViewModel = new ReservationViewModel();
            recommendedCarViewModel = new RecommendedCarViewModel();
            filoViewModel = new FiloViewModel();
        }

        public ReservationViewModel reservationViewModel { get; set; }
        public RecommendedCarViewModel recommendedCarViewModel { get; set; }
        public FiloViewModel filoViewModel { get; set; }
    }
}
