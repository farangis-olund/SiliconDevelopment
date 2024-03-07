
namespace Infrastructure.Entities;

public class SwitchEntity
{
   public int Id { get; set; }
   public string DarkTitle { get; set; } = null!;
   public string DarkImage { get; set; } = null!;
   public string LightTitle { get; set; } = null!;
   public string LightImage { get; set; } = null!;

}
