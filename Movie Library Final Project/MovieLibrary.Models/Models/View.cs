﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Models
{
    public record View
    {
        public int ViewId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int WatchedTillMinute { get; set; }
    }
}
