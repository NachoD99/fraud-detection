namespace FraudDetection.Domain;

public sealed class FraudDetector
{
    private sealed class GroupInfo
    {
        public string FirstCard { get; }
        public int FirstOrderId { get; }
        public bool Fraud { get; private set; }

        public GroupInfo(string firstCard, int firstOrderId)
        {
            FirstCard = firstCard;
            FirstOrderId = firstOrderId;
            Fraud = false;
        }

        public void MarkFraud() => Fraud = true;
    }

    public IReadOnlyCollection<int> DetectFraudulentOrderIds(IReadOnlyCollection<Order> orders)
    {
        var fraudulent = new HashSet<int>();

        var byEmail = new Dictionary<string, GroupInfo>();
        var byAddress = new Dictionary<string, GroupInfo>();

        foreach (var o in orders)
        {
            var card = Normalizer.NormalizeCard(o.CardNumber);

            var emailKey = o.DealId + "|" + Normalizer.NormalizeEmail(o.Email);
            ProcessGroup(byEmail, emailKey, card, o.OrderId, fraudulent);

            var addrKey = o.DealId + "|" + Normalizer.BuildAddressKey(o.Street, o.City, o.State, o.Zip);
            ProcessGroup(byAddress, addrKey, card, o.OrderId, fraudulent);
        }

        return fraudulent;
    }

    private static void ProcessGroup(
        Dictionary<string, GroupInfo> groups,
        string key,
        string card,
        int orderId,
        HashSet<int> fraudulent)
    {
        if (!groups.TryGetValue(key, out var info))
        {
            groups[key] = new GroupInfo(card, orderId);
            return;
        }

        if (info.Fraud)
        {
            fraudulent.Add(orderId);
            return;
        }

        if (!string.Equals(info.FirstCard, card, StringComparison.Ordinal))
        {
            info.MarkFraud();
            fraudulent.Add(info.FirstOrderId);
            fraudulent.Add(orderId);
        }
    }
}
