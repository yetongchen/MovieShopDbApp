using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieCreateModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Overview { get; set; }
        public string Tagline { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Runtime must be greater than 0")]
        public int Runtime { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a non-negative number")]
        public decimal Budget { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Revenue must be a non-negative number")]
        public decimal Revenue { get; set; }

        [Required(ErrorMessage = "Poster URL is required")]
        public string PosterUrl { get; set; }

        public string BackdropUrl { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Original Language is required")]
        public string OriginalLanguage { get; set; }

        // Genre IDs for this movie, to be assigned to movie genres in MovieGenres table
        public List<int> GenreIds { get; set; } = [];
    }
}
