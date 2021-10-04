using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain.Entity;

namespace Core
{
    public interface IRepository<T> where T : AggregateRoot
    {
        void Add(T entity);
        void Update(T entity);

        ValueTask<T> GetAndThrow(Guid id);
        
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetAndThrow(Expression<Func<T, bool>> predicate);

        Task<TDto> Get<TDto>(Expression<Func<T, bool>> predicate);
        Task<TDto> GetAndThrow<TDto>(Expression<Func<T, bool>> predicate);

        Task<List<TDto>> GetList<TDto>(Expression<Func<T, bool>> predicate);
        
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}