namespace FraudDetection.Domain;

public static class OrderParser
{
    public static Order Parse(string line)
    {
        /*
            Lines input example:
                3
                1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010
                2,1,elmer@fudd.com,123 Sesame St.,New York,NY,10011,10987654321
                3,2,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010
        */
        var parts = line.Split(',');

        if (parts.Length != 8)
            throw new FormatException($"Invalid format record. 8 fields are expected: {line}");

        int orderId = int.Parse(parts[0].Trim());
        int dealId = int.Parse(parts[1].Trim());

        string email = parts[2].Trim();
        string street = parts[3].Trim();
        string city = parts[4].Trim();
        string state = parts[5].Trim();
        string zip = parts[6].Trim();
        string card = parts[7].Trim();

        return new Order(orderId, dealId, email, street, city, state, zip, card);
    }
}
