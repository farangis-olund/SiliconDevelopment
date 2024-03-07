
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class IntegrationRepository(DataContext context) : Repo<DataContext, IntegrationEntity>(context)
{
    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<IntegrationEntity> result = await _context.Integration
            .Include(i => i.IntegrationTools)
            .ToListAsync();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
