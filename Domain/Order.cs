namespace FraudDetection.Domain;

public class Order
{
    public int OrderId { get; }
    public int DealId { get; }
    public string Email { get; }
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Zip { get; }
    public string CardNumber { get; }

    public Order(int orderId, int dealId, string email, string street, string city, string state, string zip, string cardNumber)
    {
        OrderId = orderId;
        DealId = dealId;
        Email = email;
        Street = street;
        City = city;
        State = state;
        Zip = zip;
        CardNumber = cardNumber;
    }
}
