using HakkuPaysaAPI.DTOs;
using HakkuPaysaAPI.DTOs.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Entities
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Summary { get; set; }
        public string Pic { get; set; }
        public int Likes { get; set; }
        public IList<Comment> Comments { get; set; }
        public PostDto ToDto() => new PostDto()
        {
            Id = Id,
            CreatedOn = CreatedOn,
            UpdatedOn = UpdatedOn,
            Title = Title,
            Username = Username,
            Summary = Summary,
            Pic = Pic,
            Likes = Likes,
            Comments = Comments
        };
    }
}
