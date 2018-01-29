using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmoviApp.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Display(Name = "Izvorni naziv")]
        public string IzvorniNaziv { get; set; }
        [Range(1895, 2025, ErrorMessage = "Kriva godina!")]
        public int Godina { get; set; }
        [Display(Name = "Vrijeme trajanja")]
        [DataType(DataType.Time)]
        public DateTime Trajanje { get; set; }
        public int ZanrId { get; set; }
        [Display(Name ="Žanr")]
        public Zanr Zanr { get; set; }
        public int RedateljId { get; set; }
        public Redatelj Redatelj { get; set; }
    }
}
