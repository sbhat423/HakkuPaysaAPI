using HakkuPaysaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs.UserProfileDto
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
        public DateTime DOB { get; set; }
        public string Intro { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
