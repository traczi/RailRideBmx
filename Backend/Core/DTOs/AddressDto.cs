namespace Core.DTOs;

public class AddressDto
{
    public Guid AddressId { get; set; }
    public string Name { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    
    public Guid CartId { get; set; }
}