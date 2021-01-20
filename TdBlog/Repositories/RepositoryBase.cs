using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TdBlog.Models;
using TdBlog.Shared;

namespace TdBlog.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : EntityBase<TKey>
    {
        void Save(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdOrDefaultAsync(TKey id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TResult> QueryMany<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
        IQueryable<TEntity> QueryMany(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TResult> QueryAll<TResult>(Expression<Func<TEntity, TResult>> selector);
        IQueryable<TEntity> QueryAll();
    }

    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        private AppDbContext AppDbContext { get; }
        private DbSet<TEntity> DbSet { get; }

        protected RepositoryBase(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            DbSet = AppDbContext.Set<TEntity>();
        }

        public virtual void Save(TEntity entity)
        {
            if (entity.IsNew() && entity.CreatedAt.IsEmpty())
            {
                entity.CreatedAt = DateTimeOffset.UtcNow;
            }
            else if (!entity.IsNew())
            {
                var entityEntry = AppDbContext.Entry(entity);
                entity.UpdatedAt = entityEntry.State switch
                {
                    EntityState.Modified when !entityEntry.Property(nameof(EntityBase<TKey>.UpdatedAt)).IsModified => DateTimeOffset.UtcNow,
                    EntityState.Detached => throw new InvalidOperationException("Can't save detached entity."),
                    _ => entity.UpdatedAt,
                };
            }

            DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.SingleAsync(entity => entity.Id.Equals(id));
        }

        public async Task<TEntity> GetByIdOrDefaultAsync(TKey id)
        {
            return await DbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public IQueryable<TResult> QueryMany<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return DbSet.Where(predicate).Select(selector);
        }

        public IQueryable<TEntity> QueryMany(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TResult> QueryAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return DbSet.Select(selector);
        }

        public IQueryable<TEntity> QueryAll()
        {
            return DbSet;
        }
    }
}