namespace Store.S_02.Core.Entities.Order;

public class Address
{
    public Address()
    {
        
    }
    public Address(string firstName, string lastName, string street, string city, string count)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        Count = count;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Count { get; set; }
}