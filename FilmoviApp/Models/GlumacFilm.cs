using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmoviApp.Models
{
    public class GlumacFilm
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int GlumacId { get; set; }
        public Glumac Glumac { get; set; }
    }
}
