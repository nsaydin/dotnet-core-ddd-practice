using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core;
using Core.Domain.Entity;
using Core.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Repository
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly DbSet<T> DbSet;

        protected Repository(DbContext dbContext) => DbSet = dbContext.Set<T>();

        public virtual void Add(T entity) => DbSet.Add(entity);

        public virtual void Update(T entity) => DbSet.Update(entity);

        public ValueTask<T> GetAndThrow(Guid id) => DbSet.FindAsync(id);

        public Task<TDto> Get<TDto>(Expression<Func<T, bool>> predicate) =>
            DbSet.Where(predicate).ProjectToType<TDto>().FirstOrDefaultAsync();

        public async Task<TDto> GetAndThrow<TDto>(Expression<Func<T, bool>> predicate)
        {
            var entity = await DbSet.Where(predicate).ProjectToType<TDto>().FirstOrDefaultAsync();

            if (entity == null)
                throw new ValidationException(SharedError.EntityNotFound);

            return entity;
        }

        public virtual Task<T> Get(Expression<Func<T, bool>> predicate) => DbSet.FirstOrDefaultAsync(predicate);

        public virtual async Task<T> GetAndThrow(Expression<Func<T, bool>> predicate)
        {
            var entity = await DbSet.FirstOrDefaultAsync(predicate);

            if (entity == null)
                throw new ValidationException(SharedError.EntityNotFound);

            return entity;
        }

        public Task<List<TDto>> GetList<TDto>(Expression<Func<T, bool>> predicate)
        {
            var query = DbSet.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            return query.ProjectToType<TDto>().ToListAsync();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => DbSet.AnyAsync(predicate);
    }
}