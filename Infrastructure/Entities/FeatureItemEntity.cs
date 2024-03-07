using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class FeatureItemEntity
{
	public int Id { get; set; }
	public int FeatureId { get; set; }
	public FeatureEntity Feature { get; set; } = null!;
	public string ImgUrl { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Text { get; set; } = null!;

    public static implicit operator FeatureItemEntity(FeatureItem featureItem)
    {
        return new FeatureItemEntity
        {
            FeatureId = featureItem.FeatureId,
            ImgUrl = featureItem.ImgUrl,
            Title = featureItem.Title,
            Text = featureItem.Text

        };
    }
}
