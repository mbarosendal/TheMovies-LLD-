using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Forestilling
    {
        public string Biograf { get; set; }
        public string By { get; set; }
        public string Biografsal { get; set; }
        public string Dag { get; set; }
        public DateTime Starttid { get; set; }
        public DateTime Sluttid { get; set; }
        public Movie Movie { get; set; }
    }
}
