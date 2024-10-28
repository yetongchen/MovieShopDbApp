using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }
        public required string Gender { get; set; }
        public required string Name { get; set; }
        public required string ProfilePath { get; set; }
        public required string TmdbUrl { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; } = [];
    }
}
