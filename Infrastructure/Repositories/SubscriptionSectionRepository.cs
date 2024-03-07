
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SubscriptionSectionRepository(DataContext context) : Repo<DataContext, SubscriptionSectionEntity>(context)
{
}
