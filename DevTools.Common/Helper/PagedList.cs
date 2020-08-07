using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Common.Helper
{
    public class PagedList<T>
    {
        public Meta Meta { get; set; }

        public List<T> Items { get; }

        public PagedList(IEnumerable<T> items, Meta meta)
        {
            Meta = new Meta
            {
                TotalCount = meta.TotalCount,
                PageSize = meta.PageSize,
                CurrentPage = meta.CurrentPage,
                TotalPages = (int)Math.Ceiling(meta.TotalCount / (double)meta.TotalPages),
            };

            Items = items.ToList();
        }

        public PagedList<TDest> MapTo<TDest>(IMapper mapper)
        {
            var items = mapper.Map<List<TDest>>(Items);

            return new PagedList<TDest>(items, new Meta { TotalCount = Meta.TotalCount, CurrentPage = Meta.CurrentPage, PageSize = Meta.PageSize, TotalPages = Meta.TotalPages });
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize, int count, CancellationToken cancellationToken)
        {
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling((decimal)count / pageSize);

            return new PagedList<T>(items, new Meta { TotalCount = count, CurrentPage = pageNumber, PageSize = pageSize, TotalPages = totalPages });
        }
    }
}