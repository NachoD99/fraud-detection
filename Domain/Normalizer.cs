using System.Text.RegularExpressions;

namespace FraudDetection.Domain;

public static class Normalizer
{
    private static readonly Regex MultiSpace = new(@"\s+", RegexOptions.Compiled);

    public static string NormalizeEmail(string email)
    {
        // For case insensitive
        email = email.Trim().ToLowerInvariant();

        // Detect that we have user and domain
        int at = email.IndexOf('@');
        if (at <= 0 || at == email.Length - 1)
            return email;

        string local = email.Substring(0, at);
        string domain = email.Substring(at + 1);

        // If it has a '+' ignore it and everything after
        int plus = local.IndexOf('+');
        if (plus >= 0) local = local.Substring(0, plus);

        // Then if it has a '.' remove it
        local = local.Replace(".", string.Empty);
        
        //Re-build the email
        return local + "@" + domain;
    }

    public static string NormalizeStreet(string street)
    {
        // For case insensitive
        string st = street.Trim().ToLowerInvariant();
        //Delete dot → "St." == "St"
        st = st.Replace(".", "");
        //Leave just one space
        st = MultiSpace.Replace(st, " "); 

        // In this case only works for this two cases → Street/St and Road/Rd
        st = ReplaceWord(st, "st", "street");
        st = ReplaceWord(st, "street", "street");

        st = ReplaceWord(st, "rd", "road");
        st = ReplaceWord(st, "road", "road");

        return st;
    }

    public static string NormalizeCity(string city)
    {
        // Case insensitive
        string c = city.Trim().ToLowerInvariant();
        // Leave one space
        c = MultiSpace.Replace(c, " ");
        return c;
    }

    public static string NormalizeState(string state)
    {
        // Case insensitive
        string ss = state.Trim().ToLowerInvariant();

        // Only for the states mentioned in the document
        if (ss == "illinois" || ss == "il") return "il";
        if (ss == "california" || ss == "ca") return "ca";
        if (ss == "new york" || ss == "ny") return "ny";

        return ss;
    }

    public static string NormalizeZip(string zip) => zip.Trim();

    public static string NormalizeCard(string card) => card.Trim();

    private static string ReplaceWord(string input, string word, string replacement)
    {
        var parts = input.Split(' ');
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] == word)
                parts[i] = replacement;
        }
        return string.Join(" ", parts);
    }

    public static string BuildAddressKey(string street, string city, string state, string zip)
    {
        string st = NormalizeStreet(street);
        string ci = NormalizeCity(city);
        string ss = NormalizeState(state);
        string zi = NormalizeZip(zip);

        return st + "|" + ci + "|" + ss + "|" + zi;
    }
}
