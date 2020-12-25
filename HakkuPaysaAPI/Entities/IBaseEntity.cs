using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Entities
{
    public interface IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
