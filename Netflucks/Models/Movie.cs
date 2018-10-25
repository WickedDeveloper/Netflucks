using System;
using System.Collections.Generic;

namespace Netflucks.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Comment = new HashSet<Comment>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Genre { get; set; }
        public int? Rating { get; set; }

        public ICollection<Comment> Comment { get; set; }
    }
}
