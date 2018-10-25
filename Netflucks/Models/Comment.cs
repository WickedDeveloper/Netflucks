using System;
using System.Collections.Generic;

namespace Netflucks.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int MovieId { get; set; }
        public string MovieComment { get; set; }

        public Movie Movie { get; set; }
    }
}
