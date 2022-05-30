using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class VehicleViewModel
    {
        public VehicleViewModel()
        {
            reservationViewModel = new ReservationViewModel();
            filoViewModel = new FiloViewModel();
        }

        public ReservationViewModel reservationViewModel { get; set; }
        public FiloViewModel filoViewModel { get; set; }
    }
}
