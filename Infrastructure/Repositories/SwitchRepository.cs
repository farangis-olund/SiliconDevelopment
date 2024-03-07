
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SwitchRepository(DataContext context) : Repo<DataContext, SwitchEntity>(context)
{
}
