
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
	public class AuthorRepository(DataContext context) : Repo<DataContext, AuthorEntity>(context)
	{
	}
}
