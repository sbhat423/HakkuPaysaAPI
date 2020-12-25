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

        public async Task<IEnumerable<T>> GetPagedResult<T>(DbSet<T> results) where T: class, IBaseEntity
        {
            IEnumerable<T> pagedResult;



            if (_paginationOption.OrderByCreation == Order.Desc)
            {
                var reversedOrder = results.OrderByDescending(p => p.CreatedOn);
                pagedResult = await reversedOrder.Skip(_paginationOption.PageSize * _paginationOption.PageNumber)
                    .Take(_paginationOption.PageSize).ToListAsync();

            }
            else 
            {
                pagedResult = await results
                    .Skip(_paginationOption.PageSize * _paginationOption.PageNumber)
                    .Take(_paginationOption.PageSize).ToListAsync();
            }


            return pagedResult;

        }

        
    }
}
