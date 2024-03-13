
using Infrastructure.Dtos;

namespace Infrastructure.Models;

public class CourseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Ingress { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Author { get; set; } = null!;
	public DateTime CreatedDate { get; set;}
	public double Price { get; set; }
	public string Duration { get; set; } = null!;
	public Category Category { get; set; } = null!;
}
