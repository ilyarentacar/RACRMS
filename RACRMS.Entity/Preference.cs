using System;
using System.Collections.Generic;

namespace RACRMS.Entity
{
    public partial class Preference
    {
        public Preference()
        {
            CarPreference = new HashSet<CarPreference>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<CarPreference> CarPreference { get; set; }
    }
}
