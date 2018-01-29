using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmoviApp.Models
{
    public class Zanr
    {
        public int ZanrId { get; set; }
        [Required]
        public string Naziv { get; set; }
    }
}
