using MDR.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MDR.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static TEntity GetExisting<TEntity>(this IEnumerable<TEntity> queryable, Func<TEntity, bool> predicate)
        {
            var item = queryable.FirstOrDefault(predicate);
            return item == null ? throw new NotFoundException(string.Format("No {0} found", typeof(TEntity).Name)) : item;
        }

        public static TEntity GetExisting<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicate)
        {
            var item = queryable.FirstOrDefault(predicate);
            return item == null ? throw new NotFoundException(string.Format("No {0} found", typeof(TEntity).Name)) : item;
        }

        public static async Task<TEntity> GetExistingAsync<TEntity>(this IQueryable<TEntity> queryable, CancellationToken cancellationToken = default)
        {
            var item = await queryable.FirstOrDefaultAsync(cancellationToken);
            return item == null ? throw new NotFoundException(string.Format("No {0} found", typeof(TEntity).Name)) : item;
        }

        public static async Task<TEntity> GetExistingAsync<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var item = await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
            return item == null ? throw new NotFoundException(string.Format("No {0} found", typeof(TEntity).Name)) : item;
        }
    }
}
