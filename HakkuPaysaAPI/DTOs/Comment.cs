using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs
{
    public class Comment
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Guid AuthorId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CommentedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Likes { get; set; }
    }
}
