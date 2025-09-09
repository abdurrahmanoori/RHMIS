using Microsoft.EntityFrameworkCore;
using RHMIS.Application.Repositories.Base;
using RHMIS.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RHMIS.Infrastructure.Repositories.Base
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="__context"></param>
        public GenericRepository(AppDbContext _context)
        {
            this._context = _context;
            this.dbSet = _context.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<T> AddAsync(T entity)
        {
            var entities = this._context.ChangeTracker.Entries();
            await this.dbSet.AddAsync(entity);
            return entity;
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);
            return entity != null;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> ExecuteRawQueryAsync(string sqlQuery)
        {
            return await this._context.Set<T>()
                .FromSqlRaw(sqlQuery)
                .ToListAsync();
        }

        public IQueryable<T> ExecuteRawQueryQueryable(string sqlQuery)
        {
            return this._context.Set<T>()
                .FromSqlRaw(sqlQuery);
        }



        /// <inheritdoc/>
        public Task<T> UpdateAsync(T entity)
        {
            //this._context.Entry(entity).State = EntityState.Modified;
            this._context.Update(entity);
            return Task.FromResult(entity);
        }

        /// <inheritdoc/>
        public Task RemoveAsync(T entity)
        {
            this.dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public void RemoveBulkAsync(Expression<Func<T, bool>> condition)
        {
        }
        public void RemoveBulkAsync<TProperty>(List<TProperty> ids, Expression<Func<T, TProperty>> property)
        {
            this.dbSet.Where(x => ids.Contains(property.Compile()(x))).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>>
            GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = this.dbSet.AsQueryable<T>();
            if (filter != null) // If requested records are based on a condation, then this block will execute.
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public IQueryable<T>
            GetAllQueryable(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = this.dbSet.AsQueryable();
            if (filter != null) // If requested records are based on a condation, then this block will execute.
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query;
        }

        public async Task<List<T>>
            GetListAsync<T>(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T?> GetFirstOrDefaultAsync(
              Expression<Func<T, bool>> filter,
              string? includeProperties = null,
              bool tracked = true)
        {
            IQueryable<T> query;

            // Use tracked or untracked query based on the parameter
            if (tracked)
            {
                query = this.dbSet;
            }
            else
            {
                query = this.dbSet.AsNoTracking();
            }

            // Apply filtering
            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        // new method added to include lambda expression for including entities to avoid eager loading problem for nested entities
        public async Task<T?> GetFirstOrDefaultAsyncWithInclude(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool tracked = true)
        {
            IQueryable<T> query = tracked ? this.dbSet : this.dbSet.AsNoTracking();

            query = query.Where(filter);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Overloaded Method to Accept Lambda Expression for Including Entities
        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query;

            if (tracked)
            {
                query = this.dbSet;
            }
            else
            {
                query = this.dbSet.AsNoTracking();
            }

            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<T?> GetByIdAsync(long Id)
        {
            return await this.dbSet.FindAsync(Id);
        }

        public async Task<List<T>> GetWhenAsync(Expression<Func<T, bool>> condition)
        {
            return await this.dbSet.Where(condition).ToListAsync();
        }
        public async Task<List<T>> GetWhenContainsAsync<TProperty>(Expression<Func<T, TProperty>> property, List<TProperty> ids)
        {
            ids = ids.Where(x => x != null).ToList();

            var parameter = property.Parameters.First();

            var containsMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TProperty));

            var containsExpression = Expression.Call(
                null,
                containsMethod,
                Expression.Constant(ids),
                Expression.Invoke(property, parameter)
            );

            var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
            return await this.dbSet.Where(lambda).ToListAsync();
        }

        public async Task<T> GetLastOrDefaultAsync(
             Expression<Func<T, bool>> filter,
             bool tracked = true,
             Expression<Func<T, object>>[]? includeProperties = null,
             params Expression<Func<T, object>>[] orderByProperties)
        {
            IQueryable<T> query = tracked ? this.dbSet : this.dbSet.AsNoTracking();

            // Apply filter
            query = query.Where(filter);

            // Apply includes
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Apply ordering
            if (orderByProperties != null && orderByProperties.Any())
            {
                // Apply first OrderBy clause
                IOrderedQueryable<T>? orderedQuery = query.OrderBy(orderByProperties[0]);

                // Apply ThenBy for remaining properties
                for (int i = 1; i < orderByProperties.Length; i++)
                {
                    orderedQuery = orderedQuery.ThenBy(orderByProperties[i]);
                }

                // Update query to ordered query
                query = orderedQuery;
            }

            // Use LastOrDefaultAsync on the ordered query
            var result = await query.LastOrDefaultAsync();
            // return await query.LastOrDefaultAsync() ?? default!;
            return result ?? throw new NullReferenceException("No elements found, and this error comes fron GetLastOrDefaultAsync method");

        }


        public async Task<bool> RecordExistsAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return default;//await _context.Set<T>().Where(predicate).ExecuteDeleteAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AnyAsync(filter, cancellationToken);
        }
    }

}
