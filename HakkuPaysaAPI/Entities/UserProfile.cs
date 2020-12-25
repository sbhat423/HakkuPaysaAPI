using HakkuPaysaAPI.DTOs.UserProfileDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Entities
{
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Displayname { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
        public DateTime DOB { get; set; }
        public string Intro { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public UserProfileDto ToDto() => new UserProfileDto()
        {
            Id = Id,
            Username = Username,
            Displayname = Displayname,
            ProfilePic = ProfilePic,
            Status = Status,
            DOB = DOB,
            Intro = Intro,
            Address = Address,
            Contact = Contact
        };
    }
}
