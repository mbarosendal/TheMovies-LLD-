using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies_LLD_.Models
{
    public class Spilletid
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime TimeOfDay { get; set; }
        public string Summary => $"{DayOfWeek} - {TimeOfDay:HH:mm}";
    }
}
