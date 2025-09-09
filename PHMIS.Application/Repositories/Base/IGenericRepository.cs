using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PHMIS.Application.Repositories.Base
{

    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ExecuteRawQueryAsync(string sqlQuery);

        IQueryable<T> ExecuteRawQueryQueryable(string sqlQuery);
        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            string? includeProperties = null,
            bool tracked = true);

        public Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            params Expression<Func<T, object>>[] includeProperties);

        public Task<T> GetLastOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            Expression<Func<T, object>>[]? includeProperties = null,
            params Expression<Func<T, object>>[] orderByProperties);

        Task<T?> GetByIdAsync(int Id);

        public Task<List<T>> GetWhenAsync(Expression<Func<T, bool>> condition);
        public Task<List<T>> GetWhenContainsAsync<TProperty>(Expression<Func<T, TProperty>> property, List<TProperty> ids);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        Task<List<T>> GetListAsync<T>(IQueryable<T> query);

        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task RemoveAsync(T entity);

        // Task RemoveRange(IEnumerable<T> entity);

        // Task AddRanges(List<T> entity);

        /*
         public class GenericDb1Repository<T, TContext>
    where T : class
    where TContext : DbContext, new()
{
    internal TContext context;
    internal DbSet<T> dbSet;

    public GenericRepository(TContext context) {
        this.context = context;
        this.dbSet = context.set<T>();
    }
         */

        // Task<T> Get(int id);

        // Task<IReadOnlyList<T>> GetAll();

        // List<T> GetAllCourses();
        // Task Delete(T entity);
        // Task Add(T entity);
        // Task Update(T entity);

        Task<bool> RecordExistsAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultAsyncWithInclude(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);

    }

}
