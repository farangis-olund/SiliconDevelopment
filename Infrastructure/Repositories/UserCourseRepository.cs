
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class UserCourseRepository(DataContext context) : Repo<DataContext, UserCourseEntity>(context)
	{
		private new readonly DataContext _context = context;

		public override async Task<ResponseResult> GetAllAsync(Expression<Func<UserCourseEntity, bool>> predicate)
		{
			try
			{
				var result = await _context.Set<UserCourseEntity>()
					.Where(predicate)
					.ToListAsync();
				if (result.Count > 0)
					return ResponseFactory.Ok(result);
				return ResponseFactory.NotFound();

			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}
	}
}
