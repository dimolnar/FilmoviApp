using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmoviApp.Models
{
    public class Redatelj
    {
        public int RedateljId { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        public DateTime DatRod { get; set; }
        [Display(Name = "Mjesto rođenja")]
        public string MjestoRod { get; set; }
        [Display(Name = "Redatelj")]
        public string ImeIPrezime
        {
            get
            {
                return Ime + " " + Prezime;
            }
        }
    }
}
