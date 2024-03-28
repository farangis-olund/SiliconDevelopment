
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public abstract class Repo<TContext, TEntity> : IRepo<TEntity>
	where TContext : DbContext where TEntity : class
	{
		protected readonly TContext _context;

		protected Repo(TContext context)
		{
			_context = context;

		}

		public virtual async Task<ResponseResult> AddAsync(TEntity entity)
		{
			try
			{
				_context.Set<TEntity>().Add(entity);
				await _context.SaveChangesAsync();

				Debug.WriteLine($"Entity of type {typeof(TEntity).Name} added successfully: {entity}");
				return ResponseFactory.Ok(entity);
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}

		}

		public virtual async Task<ResponseResult> GetAllAsync()
		{
			try
			{
				IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
				return ResponseFactory.Ok(result);
			}
			catch (Exception ex)
			{
                return ResponseFactory.Error(ex.Message);
            }
		}

		public virtual async Task<ResponseResult> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				IEnumerable<TEntity> result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
				return ResponseFactory.Ok(result);
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}

		public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate, Func<Task<TEntity>> createIfNotFound)
		{
			try
			{
				var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

				if (entity == null)
				{
					entity = await createIfNotFound.Invoke();
					_context.Set<TEntity>().Add(entity);
				}

				return ResponseFactory.Ok(entity); 

			}
			catch (Exception ex)
			{
                return ResponseFactory.Error(ex.Message);
            }
		}

		public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
				if (entity != null)
				{
                    return ResponseFactory.Ok(entity);
                }
				return ResponseFactory.NotFound();

			}
			catch (Exception ex)
			{
                return ResponseFactory.Error(ex.Message);
            }
		}

		public virtual async Task<ResponseResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
		{
			try
			{

				var updateEntity = _context.Set<TEntity>().FirstOrDefault(predicate);
				if (updateEntity != null)
				{
                    _context.Entry(updateEntity!).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return ResponseFactory.Ok(updateEntity);
                }
				return ResponseFactory.NotFound();
            }
			catch (Exception ex)
			{
                return ResponseFactory.Error(ex.Message);
            }
		}

		public virtual async Task<ResponseResult> RemoveAsync(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();
				if (entities.Count == 0)
				{
					return ResponseFactory.NotFound();
				}

				_context.Set<TEntity>().RemoveRange(entities);
				await _context.SaveChangesAsync();

				return ResponseFactory.Ok("Successfully removed!");
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}
				
		public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var result = await _context.Set<TEntity>().AnyAsync(predicate);
                if (!result)
                    return ResponseFactory.Exists();
				return ResponseFactory.NotFound();
			}
			catch (Exception ex)
			{
                return ResponseFactory.Error(ex.Message);
            }
		}

		public virtual async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().Where(predicate).ToListAsync();
		}

	}
}

