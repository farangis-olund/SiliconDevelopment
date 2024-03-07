
namespace Infrastructure.Entities;

public class TaskMasterEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string CheckListItems { get; set; } = null!;

}
