using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs
{
    public class Comment
    {
        public string PostId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CommentId { get; set; }
        public string AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime CommentedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Likes { get; set; }
    }
}
