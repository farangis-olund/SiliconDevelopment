
namespace Infrastructure.Models;

public class Pagination<T>
{
	public int CurrentPage { get; private set; }
	public int TotalPages { get; private set; }
	public int ItemsPerPage { get; private set; }
	public int TotalItems { get; private set; }
	public List<T> Items { get; private set; }

	public Pagination(List<T> items, int currentPage, int itemsPerPage)
	{
		TotalItems = items.Count;
		TotalPages = (int)Math.Ceiling((double)TotalItems / itemsPerPage);
		CurrentPage = Math.Max(1, Math.Min(currentPage, TotalPages));
		ItemsPerPage = itemsPerPage;

		int startIndex = (CurrentPage - 1) * ItemsPerPage;
		int endIndex = Math.Min(startIndex + ItemsPerPage - 1, TotalItems - 1);

		Items = items.GetRange(startIndex, endIndex - startIndex + 1);
	}
}