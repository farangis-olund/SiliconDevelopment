namespace Infrastructure.Dtos.Sections;

public class Integration
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
}
