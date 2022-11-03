﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.MovieRequests
{
    public class UpdateMovieRequest
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
    }
}