using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Spilletid
    {
        public DayOfWeek Dag { get; set; }
        public DateTime StartTid { get; set; }
        public string Summary => $"{Dag} - {StartTid:HH:mm}";
    }
}
