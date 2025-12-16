var firstLine = Console.ReadLine();
if (firstLine is null)
    return;

int n = int.Parse(firstLine.Trim());

var lines = new List<string>(n);

for (int i = 0; i < n; i++)
{
    var line = Console.ReadLine();
    if (line is null) break;

    if (string.IsNullOrWhiteSpace(line))
    {
        i--;
        continue;
    }

    lines.Add(line);
}

Console.WriteLine($"Read {lines.Count} records");
