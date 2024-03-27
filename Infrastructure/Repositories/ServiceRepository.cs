
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
	public class ServiceRepository(DataContext context) : Repo<DataContext, ServiceEntity>(context)
	{
	}

}
