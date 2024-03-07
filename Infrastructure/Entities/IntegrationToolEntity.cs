using Infrastructure.Dtos.Sections;

namespace Infrastructure.Entities;

public class IntegrationToolEntity
{
    public int Id { get; set; }
    public int IntegrationId { get; set; }
    public IntegrationEntity Integration { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;

    public static implicit operator IntegrationToolEntity(IntegrationTool dto)
    {
        return new IntegrationToolEntity
        {
            Id = dto.Id,
            IntegrationId = dto.IntegrationId,
            Image = dto.Image,
            Description = dto.Description
        };
    }

}
