namespace Infrastructure.Entities;

public class CategoryEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public ICollection<CourseEntity> Courses { get; set; } = [];
}