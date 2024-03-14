
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class CourseRepository(DataContext context) : Repo<DataContext, CourseEntity>(context)
	{
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
	}
}