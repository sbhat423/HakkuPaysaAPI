using HakkuPaysaAPI.DTOs.PaginationDto;
using HakkuPaysaAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HakkuPaysaAPI.Utilities
{
    public class Paginator
    {
        private readonly PaginationOptionDto _paginationOption;
        public Paginator(PaginationOptionDto PaginationOption)
        {
            _paginationOption = PaginationOption;
        }

        public async Task<IEnumerable<Post>> GetPagedResult(DbSet<Post> results) 
        {
            IQueryable<Post> pagedResult;
            IEnumerable<Post> res;

            // Type myType = typeof(T);
            // Get the PropertyInfo object by passing the property name.
            // PropertyInfo myPropInfo = myType.GetProperty(_paginationOption.OrderOnProperty);

            if (_paginationOption.Order == Order.Desc) 
            {
                pagedResult = results
                    .OrderByDescending(p => p.CreatedOn)
                    .Skip(_paginationOption.PageSize * _paginationOption.PageNumber)
                    .Take(_paginationOption.PageSize);
                res = await pagedResult.ToListAsync();

            }

            pagedResult = results
                .Skip(_paginationOption.PageSize * _paginationOption.PageNumber)
                .Take(_paginationOption.PageSize);
            res = await pagedResult.ToListAsync();

            return res;

        }
    }
}
