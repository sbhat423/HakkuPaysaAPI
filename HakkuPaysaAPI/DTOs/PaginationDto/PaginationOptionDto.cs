using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.DTOs.PaginationDto
{
    public class PaginationOptionDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Order OrderByCreation { get; set; }
    }

    public enum Order {
        Asc = 0,
        Desc = 1
    }
}
