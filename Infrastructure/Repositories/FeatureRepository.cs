using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class FeatureRepository(DataContext context) : Repo<DataContext, FeatureEntity>(context)
{
    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<FeatureEntity> featureList = await _context.Features
            .Include(i => i.FeatureItems)
            .ToListAsync();

            return ResponseFactory.Ok(featureList);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }

    }
}
