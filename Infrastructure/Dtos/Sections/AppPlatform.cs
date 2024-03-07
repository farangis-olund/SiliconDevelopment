using Infrastructure.Entities;

namespace Infrastructure.Dtos.Sections;

public class AppPlatform
{
    public int Id { get; set; }
    public int DownloadAppId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Ratings { get; set; }
    public string Platform { get; set; } = null!;
    public int Reviews { get; set; }

    public static implicit operator AppPlatform(AppPlatformEntity entity)
    {
        return new AppPlatform
        {
            Id = entity.Id,
            DownloadAppId = entity.DownloadAppId,
            Name = entity.Name,
            Ratings = entity.Ratings,
            Platform = entity.Platform,
            Reviews = entity.Reviews
        };
    }
}