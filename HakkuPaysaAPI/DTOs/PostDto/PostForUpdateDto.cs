﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs.Post
{
    public class PostForUpdateDto
    {
        public string Id { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Pic { get; set; }
        public int Likes { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
