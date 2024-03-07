using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class IntegrationEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public ICollection<IntegrationToolEntity> IntegrationTools { get; set; } = [];

    public static implicit operator IntegrationEntity(Integration dto)
    {
        return new IntegrationEntity
        {
            Id = dto.Id,
            Title = dto.Title,
            Ingress = dto.Ingress

        };
    }
}
