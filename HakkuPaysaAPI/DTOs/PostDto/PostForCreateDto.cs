using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs
{
    public class PostForCreateDto
    {
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Username { get; set; }
        public string Summary { get; set; }
        public string Pic { get; set; }
    }
}
