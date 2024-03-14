using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class AppPlatformEntity
{
    public int Id { get; set; }
    public int DownloadAppId { get; set; }
    public DownloadAppEntity DownloadApp { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Ratings { get; set; }
    public string Platform { get; set; } = null!;
    public int Reviews { get; set; }

    public static implicit operator AppPlatformEntity(AppPlatform dto)
    {
        return new AppPlatformEntity
        {
            Id = dto.Id,
            DownloadAppId = dto.DownloadAppId,
            Name = dto.Name,
            Ratings = dto.Ratings,
            Platform = dto.Platform,
            Reviews = dto.Reviews

        };
    }
}
