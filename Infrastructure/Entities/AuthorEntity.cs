namespace Infrastructure.Entities;

public class AuthorEntity
{
	public int Id { get; set; }
	public string AuthorName { get; set; } = null!;
	public string AuthorDescription { get; set; } = null!;
	public int Subscribers { get; set; }
	public int Followers { get; set; }
	public ICollection<CourseEntity> Courses { get; set; } = [];

}
