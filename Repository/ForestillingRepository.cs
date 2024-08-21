using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies_LLD_.Models;

namespace TheMovies_LLD_.Repository
{
    public class ForestillingRepository
    {
        public List<Forestilling> forestillinger;

        public ForestillingRepository()
        {
            // testdata
            forestillinger = new List<Forestilling>();
            //{
            //    new Forestilling { Biograf = "Nordisk Film", By="Aarhus", Biografsal = "1", Dag="Friday", Klokken = "12:00", Movie = "Titanic" },
            //    new Forestilling { Biograf = "Nordisk Film", By="Randers", Biografsal= "1", Dag="Friday", Klokken = "12:00", Movie = "Titanic" },
            //};
        }

        public void AddForestilling(Forestilling forestilling)
        {
            forestillinger.Add(forestilling);
        }

        public List<Forestilling> GetAllForestillinger()
        {
            return forestillinger;
        }

        // Tjek om der allerede findes en forestilling med samme biograf, by, biografsal, dag og starttid, og som ligger inden for samme tidsinterval
        public bool AreForestillingerOverlapping(Forestilling forestilling, DateTime startTime, DateTime endTime)
        {
            return forestillinger.Any(f =>
                f.Biograf == forestilling.Biograf &&
                f.By == forestilling.By &&
                f.Biografsal == forestilling.Biografsal &&
                f.Dag == forestilling.Dag &&
                (
                // Tjekker om en forestilling starter før en anden slutter, og om en forestilling slutter efter en anden starter
                    (f.Starttid < endTime && f.Sluttid > startTime)
                )
            );
        }
    }
}
