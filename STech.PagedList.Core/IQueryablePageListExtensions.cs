using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STech.PagedList.Core
{
    public static class IQueryablePageListExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }


            //var count = source.AsEnumerable().Count();
            //var items = source.AsEnumerable().Skip((pageIndex - indexFrom) * pageSize)
            //    .Take(pageSize).ToList();


            //var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            //var items = await source.Skip((pageIndex - indexFrom) * pageSize)
            //    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            //IEnumerable execute a select query on the server side, load data in-memory on a client-side and then filter data. and doesn’t support lazy loading
            //IQueryable execute the select query on the server side with all filters. and support lazy loading
            var count = source.AsEnumerable().Count();
            var items = source.Skip((pageIndex - indexFrom) * pageSize)
                .Take(pageSize).ToList();


            //var count = await source.CountAsync();
            //var items = await source.Skip(
            //    (pageIndex - indexFrom) * pageSize)
            //    .Take(pageSize).ToListAsync();


            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }
    }
}
