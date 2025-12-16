using FraudDetection.Domain;

var firstLine = Console.ReadLine();
if (firstLine is null)
    return;

int n = int.Parse(firstLine.Trim());

var orders = new List<Order>(n);

for (int i = 0; i < n; i++)
{
    var line = Console.ReadLine();
    if (line is null) break;

    if (string.IsNullOrWhiteSpace(line))
    {
        i--;
        continue;
    }

    orders.Add(OrderParser.Parse(line));
}

if (orders.Count > 0)
{
    foreach(var o in orders) {
        Console.WriteLine($"#{o.OrderId} order: id={o.OrderId}, deal={o.DealId}, email={o.Email}, card={o.CardNumber}");
    }
}
else Console.WriteLine("No orders read");
