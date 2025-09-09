
namespace PHMIS.Application.Common
{

    public class PagedList<T>
    {
        public static int DefaultPageSize = 25;
        public static int DefaultPageNumber = 1;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        public static async Task<PagedList<T>> CreateList(IQueryable<T> entities, int pageNumber, int pageSize)
        {
            var count = entities.Count();
            return new PagedList<T>()
            {
                PageNumber = pageNumber > 0 ? pageNumber : DefaultPageNumber,
                PageSize = pageSize > 0 ? pageSize : DefaultPageSize,
                TotalCount = count,
                TotalPages = CalculateTotalPages(count, pageSize),
                Items =  entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                //Items = await entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(),
            };
        }

        public static async Task<PagedList<T>> CreateList(IQueryable<T> entities, int pageNumber)
        {
            return await CreateList(entities, pageNumber, DefaultPageSize);
        }

        private static int CalculateTotalPages(int totalCount, int pageSize)
        {
            return (int)Math.Ceiling((double)totalCount / pageSize);
        }
    }

    public static class PagedListExtensions
    {
        public static async Task<PagedList<TEntity>> ToPagedList<TEntity>(this IQueryable<TEntity> queryable, int pageNumber,
            int pageSize)
        {
            return await PagedList<TEntity>.CreateList(queryable, pageNumber, pageSize);
        }

        public static async Task<PagedList<TEntity>> ToPagedList<TEntity>(this IQueryable<TEntity> queryable, int pageNumber)
        {
            return await PagedList<TEntity>.CreateList(queryable, pageNumber);
        }
    }
}
