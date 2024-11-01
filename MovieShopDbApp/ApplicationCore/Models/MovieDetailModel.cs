﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieDetailModel
    {
        public required int Id { get; set; }
        public string TmdbUrl { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public int Runtime { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public string BackdropUrl { get; set; }
        public string PosterUrl { get; set; }
        public string ImdbUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Genres { get; set; } = [];
        public List<CastModel> Casts { get; set; } = [];
        public List<TrailerModel> Trailers { get; set; } = [];
        public bool IsPurchased { get; set; }
    }

    public class CastModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }
        public string ProfilePath { get; set; }
    }

    public class TrailerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrailerUrl { get; set; }
    }
}
