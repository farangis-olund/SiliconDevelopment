using Infrastructure.Entities;

namespace Infrastructure.Dtos.Sections;

public class DownloadApp
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Image { get; set; } = null!;
    public List<AppPlatform> Platforms { get; set; } = [];

    public static implicit operator DownloadApp(DownloadAppEntity entity)
    {
        return new DownloadApp
        {
            Id = entity.Id,
            Title = entity.Title,
            Image = entity.Image,
            Platforms = entity.Platforms.Select(fi => (AppPlatform)fi).ToList()
        };
    }
}
