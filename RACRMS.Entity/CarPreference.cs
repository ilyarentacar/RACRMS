using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class CarPreference
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int PreferenceId { get; set; }

        public Car Car { get; set; }
        public Preference Preference { get; set; }
    }
}
