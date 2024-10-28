using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public required int MovieId { get; set; }
        public required string Name { get; set; }
        public required string TrailerUrl { get; set; }

        public Movie Movie { get; set; }
    }
}
