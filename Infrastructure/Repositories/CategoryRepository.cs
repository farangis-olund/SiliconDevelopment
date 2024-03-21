
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
	public class CategoryRepository(DataContext context) : Repo<DataContext, CategoryEntity>(context)
	{

	}
}
