
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
	public class SubscriptionRepository(DataContext context) : Repo<DataContext, SubscriptionEntity>(context)
	{
	}
}
