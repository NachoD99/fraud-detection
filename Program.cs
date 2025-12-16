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

var detector = new FraudDetector();
var fraudulentIds = detector.DetectFraudulentOrderIds(orders);

var output = fraudulentIds
    .OrderBy(id => id)
    .Select(id => id.ToString())
    .ToArray();

Console.WriteLine($"Fraudulent order ids: {string.Join(", ", output)}");

