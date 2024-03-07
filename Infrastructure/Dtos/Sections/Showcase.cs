using Infrastructure.Entities;

namespace Infrastructure.Dtos.Sections;

public class Showcase
{
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string ButtonText { get; set; } = null!;
    public string ButtonLink { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Brands { get; set; } = null!;

    public static implicit operator Showcase(ShowcaseEntity entity)
    {
        return new Showcase
        {
            Title = entity.Title,
            Ingress = entity.Ingress,
            ButtonText = entity.ButtonText,
            ButtonLink = entity.ButtonLink,
            SubTitle = entity.SubTitle,
            Image = entity.Image,
            Brands = entity.Brands
        };
    }
}
