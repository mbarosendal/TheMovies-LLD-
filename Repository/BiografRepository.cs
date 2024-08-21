using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies_LLD_.Models;

namespace TheMovies_LLD_.Repository
{
    public class BiografRepository
    {
        public List<Biograf> Biografer { get; set; }

        public BiografRepository()
        {
            // testdata (3 biografer med hver deres biografsale og spilletider)
            Biografer = new List<Biograf>
            {
                new Biograf
                {
                    Biografkæde = "Nordisk Film",
                    By = "Aarhus",
                    TelefonNummer = "12345678",
                    Email = "service@nordiskfilm.dk",
                    Biografsale = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Wednesday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Wednesday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Wednesday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            Id = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Thursday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Thursday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Thursday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            }
                        }
                    }
                },

                new Biograf
                {
                    Biografkæde = "Cinemaxx",
                    By = "Randers",
                    TelefonNummer = "12345678",
                    Email = "info@cinemaxx.dk",
                   Biografsale = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Friday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Friday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Friday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            Id = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Saturday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Saturday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Saturday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            }
                        },
                        new Biografsal
                        {
                            Id = "3",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Sunday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Sunday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Sunday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            }
                        }
                    }
                },

                new Biograf
                {
                    Biografkæde = "Palads",
                    By = "Odense",
                    TelefonNummer = "12345678",
                    Email = "kontakt@palads.dk",
                    Biografsale = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Monday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Monday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Monday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            Id = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { Dag = DayOfWeek.Tuesday, StartTid = new DateTime(1, 1, 1, 12, 0, 0) }, // Friday at 12:00
                                new Spilletid { Dag = DayOfWeek.Tuesday, StartTid = new DateTime(1, 1, 1, 16, 0, 0) }, // Friday at 16:00
                                new Spilletid { Dag = DayOfWeek.Tuesday, StartTid = new DateTime(1, 1, 1, 21, 0, 0) }  // Friday at 21:00
                            }
                        }
                    }
                }
            };
        }

        public List<Biograf> GetAllBiografer()
        {
            return Biografer;
        }
    }
}
