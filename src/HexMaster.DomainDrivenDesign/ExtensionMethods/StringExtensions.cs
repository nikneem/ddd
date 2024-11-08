using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HexMaster.DomainDrivenDesign.ExtensionMethods;

internal static  class StringExtensions
{

    public static bool IbanChecksumCheck(this string iban)
    {
        string ibanCleared = iban.ToCondensedString();
        string ibanSwapped = ibanCleared.Substring(4) + ibanCleared.Substring(0, 4);
        string sum = ibanSwapped.Aggregate("", (current, c) => current + (char.IsLetter(c) ? (c - 55).ToString() : c.ToString()));

        var d = decimal.Parse(sum);
        return d % 97 == 1;
    }

    internal static string ToCondensedString(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        const string alphaNumericPattern = @"([0-9a-zA-Z+*]+)";

        var cleaningResult = Regex.Matches(value, alphaNumericPattern);
        if (cleaningResult.Count == 0)
            return string.Empty;

        var output = new StringBuilder();
        foreach (Match m in cleaningResult)
        {
            output.Append(FormatMatchResult(m));
        }

        return output.ToString();
    }
    static string FormatMatchResult(Match m)
    {
        return m.Index == 0 && string.Equals(m.Value, "IBAN", StringComparison.OrdinalIgnoreCase)
            ? string.Empty
            : m.Value;
    }
}