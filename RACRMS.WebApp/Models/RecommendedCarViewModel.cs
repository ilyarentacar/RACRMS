using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.WebApp.Models
{
    public class RecommendedCarViewModel
    {
        public RecommendedCarViewModel()
        {
            CarDescriptions = new List<string>();

            CarBrandName = "Volkswagen";
            CarModelName = "Polo 1.4 TSI";
            CarRentalPrice = 400;
            CarImage = "https://www.avis.com.tr/Avis/media/Avis/Cars/b-fiat-egea-cross.png";
            CarDescriptions.Add("Ne kadar dikkatli bakarsanız bakın, bazıları göründüğünden fazlasıdır. Modern dokunuşlarla yeniden yorumlanan kompakt gövdesinin ardında sakladığı geniş iç mekanı ve yükleme kapasitesi, premium otomobillerde görmeye alışık olduğumuz teknolojileri, hiçbir zaman değişmeyen Volkswagen üretim kalitesi ve sağlamlığıyla Yeni Polo, şimdi sizi bekliyor.");
        }

        public string CarBrandName { get; set; }
        public string CarModelName { get; set; }
        public decimal CarRentalPrice { get; set; }
        public string CarImage { get; set; }
        public List<string> CarDescriptions { get; set; }
    }
}
