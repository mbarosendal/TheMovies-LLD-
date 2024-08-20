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
                            ID = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Wednesday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Wednesday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Wednesday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            ID = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Thursday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Thursday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Thursday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
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
                            ID = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Friday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Friday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Friday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            ID = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Saturday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Saturday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Saturday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
                            }
                        },
                        new Biografsal
                        {
                            ID = "3",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Sunday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Sunday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Sunday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
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
                            ID = "1",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Monday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Monday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Monday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
                            },
                        },
                        new Biografsal
                        {
                            ID = "2",
                            Spilletider = new List<Spilletid>
                            {
                                new Spilletid { DayOfWeek = DayOfWeek.Tuesday, TimeOfDay = new TimeSpan(12, 0, 0) }, // Friday at 12:00
                                new Spilletid { DayOfWeek = DayOfWeek.Tuesday, TimeOfDay = new TimeSpan(16, 0, 0) }, // Friday at 16:00
                                new Spilletid { DayOfWeek = DayOfWeek.Tuesday, TimeOfDay = new TimeSpan(21, 0, 0) }  // Friday at 21:00
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
