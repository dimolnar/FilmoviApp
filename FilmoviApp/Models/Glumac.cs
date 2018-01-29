using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmoviApp.Models
{
    public class Glumac
    {
        public int GlumacId { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Display(Name = "Rodno ime")]
        public string RodnoIme { get; set; }
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        public DateTime DatRod { get; set; }
        [Display(Name = "Mjesto rođenja")]
        public string MjestoRod { get; set; }
        [Display(Name = "Glumac")]
        public string ImeIPrezime
        {
            get
            {
                return Ime + " " + Prezime;
            }
        }
    }
}
