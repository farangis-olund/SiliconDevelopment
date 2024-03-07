
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class DownloadAppRepository(DataContext context) : Repo<DataContext, DownloadAppEntity>(context)
{
    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<DownloadAppEntity> result = await _context.DownloadApp
                .Include(x => x.Platforms)
                .ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
