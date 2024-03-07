using Infrastructure.Entities;

namespace Infrastructure.Dtos.Sections;

public class Feature
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;

    public List<FeatureItem> FeatureItems { get; set; } = [];

    public static implicit operator Feature(FeatureEntity entity)
    {
        return new Feature
        {
            Id = entity.Id,
            Title = entity.Title,
            Ingress = entity.Ingress,
            FeatureItems = entity.FeatureItems.Select(fi => (FeatureItem)fi).ToList()
        };
    }
}
