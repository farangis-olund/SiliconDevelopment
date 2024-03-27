
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
	public class ApiUserRepository(DataContext context) : Repo<DataContext, ApiUserEntity>(context)
	{
	}

}
