using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Biografsal
    {
        public string Id { get; set; }
        // Hver biografsal har en liste med spilletider
        public List<Spilletid> Spilletider { get; set; }
    }
}
