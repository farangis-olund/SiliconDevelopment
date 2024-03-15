
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class CourseRepository(DataContext context) : Repo<DataContext, CourseEntity>(context)
	{
		private new readonly DataContext _context = context;
		
		public override async Task<ResponseResult> GetAllAsync()
		{
			try
			{
				IEnumerable<CourseEntity> result = await _context.Course
					.Include(x => x.Author)
					.Include(x => x.Category)
					.ToListAsync();
				return ResponseFactory.Ok(result);
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}

		public override async Task<ResponseResult> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
		{
			try
			{
				var result = await _context.Course
					.Include(x => x.Author)
					.Include(x => x.Category)
					.FirstOrDefaultAsync(predicate);


				return ResponseFactory.Ok(result!);
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}
	}
}