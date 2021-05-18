using Mercadinho.Prateleira.Infrastructure.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.Infrastructure.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IAsyncDisposable where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await _dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            return entityEntry.Entity;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default) => 
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;

        public void Delete(TEntity entity) => _dbContext.Entry(entity).State = EntityState.Deleted;

        public async ValueTask DisposeAsync() => 
            await _dbContext.DisposeAsync().ConfigureAwait(false);

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "", bool noTracking = false, int? take = null, 
            int? skip = null)
        {
            IQueryable<TEntity> query = this._dbSet;

            if (noTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query.Take(take.Value);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            bool noTracking = false,
            int? take = null,
            int? skip = null,
            CancellationToken cancellationToken = default) => 
                await GetAll(filter, orderBy, includeProperties, noTracking, take, skip)
                    .ToListAsync(cancellationToken).ConfigureAwait(false);

        public async ValueTask<TEntity> GetByKeysAsync(
            CancellationToken cancellationToken = default, 
            params object[] keys) => 
                await _dbSet.FindAsync(keys, cancellationToken).ConfigureAwait(false);

        public void Update(TEntity entity) => 
            _dbContext.Entry(entity).State = EntityState.Modified;

    }
}
