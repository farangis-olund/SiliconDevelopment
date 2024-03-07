using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class ShowcaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string ButtonText { get; set; } = null!;
    public string ButtonLink { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Brands { get; set; } = null!;
   
    public static implicit operator ShowcaseEntity(Showcase showcase)
    {
        return new ShowcaseEntity
        {
            Title = showcase.Title,
            Ingress = showcase.Ingress,
            ButtonText = showcase.ButtonText,
            ButtonLink = showcase.ButtonLink,
            Image = showcase.Image,
            SubTitle = showcase.SubTitle,
            Brands = showcase.Brands
           
        };
    }

}
