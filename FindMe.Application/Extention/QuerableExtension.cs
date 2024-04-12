using FindMe.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Application.Extention
{
    public static class QuerableExtension
    {
        public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            int count = await source.CountAsync();
            pageSize = pageSize == 0 ? 10 : pageSize;

            List<T> items = pageNumber > 1 ? await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken)
            : await source.Take(pageSize).ToListAsync(cancellationToken);


            return PaginatedResponse<T>.Create(items, count, pageNumber, pageSize);
        }
    }
}
