using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Biograf
    {
        public string Biografkæde { get; set; }
        public string By { get; set; }
        public string TelefonNummer { get; set; }
        public string Email { get; set; }
        // Hver biograf har en liste med biografsale
        public List<Biografsal> Biografsale { get; set; }
        public string Summary => $"{Biografkæde} ({By})";
    }
}
