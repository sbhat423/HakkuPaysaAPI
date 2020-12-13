using HakkuPaysaAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public string Summary { get; set; }
        public string Pic { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
