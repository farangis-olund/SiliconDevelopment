using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class DownloadAppEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Image { get; set; } = null!;
    public ICollection<AppPlatformEntity> Platforms { get; set; } = [];

    public static implicit operator DownloadAppEntity(DownloadApp dto)
    {
        return new DownloadAppEntity
        {
            Id = dto.Id,
            Title = dto.Title,
            Image = dto.Image
        };
    }

}
