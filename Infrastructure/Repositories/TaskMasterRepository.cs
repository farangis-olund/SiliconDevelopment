
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class TaskMasterRepository(DataContext context) : Repo<DataContext, TaskMasterEntity>(context)
{
}