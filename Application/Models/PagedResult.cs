using Microsoft.EntityFrameworkCore;

namespace Application.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int PageSize { get; set; } 


        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageSize = Items.Count();
        }
        public static async Task<PagedResult<T>> CreateAsync(
            IQueryable<T> source, int? index , int? size)
        {
            var pageIndex = index ?? 1;
            var pageSize = size ?? 6;
            var count = await source.CountAsync();
            var items = await source.Skip(
                    (int)((pageIndex - 1) * pageSize)!)
                .Take((int)pageSize!).ToListAsync();
            return new PagedResult<T>(items, count,(int) pageIndex!,(int) pageSize);
        }
    }

}
