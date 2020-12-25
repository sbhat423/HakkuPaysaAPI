using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Entities
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
    }
}
