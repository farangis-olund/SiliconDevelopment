namespace Infrastructure.Dtos;

public class Address
{
    public string Addressline_1 { get; set; } = null!;
    public string? Addressline_2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}