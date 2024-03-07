
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AppPlatformRepository(DataContext context) : Repo<DataContext, AppPlatformEntity>(context)
{
}
