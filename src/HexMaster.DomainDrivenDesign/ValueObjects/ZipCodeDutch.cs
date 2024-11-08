using System;
using System.Text.RegularExpressions;
using HexMaster.DomainDrivenDesign.Abstractions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class ZipCodeDutch : IZipCode
{
    private const string MatchPattern = "^(?<Numbers>[1-9]\\d{3})\\ *(?<Letters>[a-zA-Z]{2})$";

    public string Value { get; }

    public static IZipCode Parse(string value)
    {
        var matches = Regex.Match(value, MatchPattern);
        if (matches.Success)
        {
            var numbers = matches.Groups["Numbers"].Success ? matches.Groups["Numbers"].Value : null;
            var letters = matches.Groups["Letters"].Success ? matches.Groups["Letters"].Value : null;
            return new ZipCodeDutch($"{numbers} {letters.ToUpperInvariant()}");
        }

        throw new ArgumentException(
            $"The value {value} is not a valid dutch zip code and does not match the pattern {MatchPattern}");
    }

    public override string ToString()
    {
        return Value;
    }

    private ZipCodeDutch(string value)
    {
        Value = value;
    }
}