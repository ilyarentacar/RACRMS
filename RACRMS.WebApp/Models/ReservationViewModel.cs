using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            StartHours = new Dictionary<string, string>();
            EndHours = new Dictionary<string, string>();

            setDictionaries();
        }

        public string StartDate { get; set; }
        public string StartHour { get; set; }
        public string EndDate { get; set; }
        public string EndHour { get; set; }

        public Dictionary<string, string> StartHours { get; set; }
        public Dictionary<string, string> EndHours { get; set; }

        private void setDictionaries()
        {
            for (int index = 0; index < 24; index++)
            {
                StartHours.Add((index.ToString().Length == 1 ? $"0{index}:00" : $"{index}:00"), (index.ToString().Length == 1 ? $"0{index}:00" : $"{index}:00"));
                EndHours.Add((index.ToString().Length == 1 ? $"0{index}:00" : $"{index}:00"), (index.ToString().Length == 1 ? $"0{index}:00" : $"{index}:00"));

                StartHours.Add((index.ToString().Length == 1 ? $"0{index}:30" : $"{index}:30"), (index.ToString().Length == 1 ? $"0{index}:30" : $"{index}:30"));
                EndHours.Add((index.ToString().Length == 1 ? $"0{index}:30" : $"{index}:30"), (index.ToString().Length == 1 ? $"0{index}:30" : $"{index}:30"));
            }
        }
    }
}
