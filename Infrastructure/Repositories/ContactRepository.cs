
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class ContactRepository(DataContext context) : Repo<DataContext, ContactEntity>(context)
	{
		public override async Task<ResponseResult> GetAllAsync()
		{
			try
			{
				var result = await _context.Contact
					.Include(x => x.Service)
					.ToListAsync();

				if (result == null)
					return ResponseFactory.NotFound();
				else
					return ResponseFactory.Ok(result!);

			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}


		public override async Task<ResponseResult> GetOneAsync(Expression<Func<ContactEntity, bool>> predicate)
		{
			try
			{
				var result = await _context.Contact
					.Include(x => x.Service)
					.FirstOrDefaultAsync(predicate);
				if (result == null)
					return ResponseFactory.NotFound();
				else
					return ResponseFactory.Ok(result!);
			}
			catch (Exception ex)
			{
				return ResponseFactory.Error(ex.Message);
			}
		}
	}

}
