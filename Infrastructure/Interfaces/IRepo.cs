using Infrastructure.Models;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IRepo<TEntity> where TEntity : class
{
	Task<ResponseResult> AddAsync(TEntity entity);
    Task<ResponseResult> GetAllAsync();
	Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
	Task<ResponseResult> RemoveAsync(Expression<Func<TEntity, bool>> predicate);
	Task<ResponseResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);
	Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
