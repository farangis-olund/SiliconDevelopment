using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class FeatureEntity
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string Ingress { get; set; }= null!;

	public ICollection<FeatureItemEntity> FeatureItems { get; set; } = [];

    public static implicit operator FeatureEntity(Feature feature)
    {
        return new FeatureEntity
        {
            Id = feature.Id,
            Title = feature.Title,
            Ingress = feature.Ingress

        };
    }
}
