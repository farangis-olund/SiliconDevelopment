
using Infrastructure.Dtos;
using System.ComponentModel;

namespace Infrastructure.Models;

public class CourseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Author { get; set; } = null!;
	public DateTime CreatedDate { get; set;}
	public decimal Price { get; set; }
	public Category Category { get; set; } = null!;
}
