using Infrastructure.Entities;

namespace Infrastructure.Dtos.Sections;

public class FeatureItem
{
    public int FeatureId { get; set; }
    public string ImgUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;

    public static implicit operator FeatureItem(FeatureItemEntity entity)
    {
        return new FeatureItem
        {
            FeatureId = entity.FeatureId,
            Title = entity.Title,
            ImgUrl = entity.ImgUrl,
            Text = entity.Text
        };
    }
}
