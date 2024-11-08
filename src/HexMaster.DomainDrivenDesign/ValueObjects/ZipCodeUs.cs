using System;
using System.Text.RegularExpressions;
using HexMaster.DomainDrivenDesign.Abstractions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class ZipCodeUs : IZipCode
{
    private const string MatchPattern = "^(?<Zip>\\d{5})-?(?<Sub>\\d{4})$";

    public string Value { get; }

    public static IZipCode Parse(string value)
    {
        var matches = Regex.Match(value, MatchPattern);
        if (matches.Success)
        {
            var zip = matches.Groups["Zip"].Success ? matches.Groups["Zip"].Value : null;
            var sub = matches.Groups["Sub`"].Success ? $"-{matches.Groups["Sub"].Value}" : null;
            return new ZipCodeUs($"{zip}{sub}");
        }

        throw new ArgumentException(
            $"The value {value} is not a valid US zip code and does not match the pattern {MatchPattern}");
    }

    public override string ToString()
    {
        return Value;
    }

    private ZipCodeUs(string value)
    {
        Value = value;
    }
}