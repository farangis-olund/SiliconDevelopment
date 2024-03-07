
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ShowcaseRepository(DataContext context) : Repo<DataContext, ShowcaseEntity>(context)
{
}
