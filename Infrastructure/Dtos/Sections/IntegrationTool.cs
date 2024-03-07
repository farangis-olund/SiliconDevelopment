namespace Infrastructure.Dtos.Sections;

public class IntegrationTool
{
    public int Id { get; set; }
    public int IntegrationId { get; set; }
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
}
