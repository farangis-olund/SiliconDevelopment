namespace Infrastructure.Dtos;

public class User
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Biography { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }

    public List<Address> AddressList { get; set; } = [];
}
