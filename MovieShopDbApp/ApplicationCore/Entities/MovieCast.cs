using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCast
    {
        public required int MovieId { get; set; }
        public required int CastId { get; set; }
        public required string Character { get; set; }

        public Movie Movie { get; set; }
        public Cast Cast { get; set; }
    }
}
