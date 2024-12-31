using AutoMapper;
using Microsoft.EntityFrameworkCore;
using demoapi.DTO;
namespace demoapi.Pagination
{
    public class PaginationMethod
    {
        public static async Task<PaginationDto<TDto>> PaginateAsync<T, TDto>(
                IQueryable<T> query,
                IMapper mapper,
                int page,
                int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationDto<TDto>
            {
                Items = mapper.Map<IEnumerable<TDto>>(items),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


    }
}

