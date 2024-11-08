using System;
using HexMaster.DomainDrivenDesign.ValueObjects;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.ValueObjects;

public class DutchZipCodeTests
{
    [Theory]
    [InlineData("1234aa", "1234 AA")]
    [InlineData("1234 aa", "1234 AA")]
    [InlineData("1234Aa", "1234 AA")]
    [InlineData("1234aB", "1234 AB")]
    [InlineData("1234 AA", "1234 AA")]
    public void WhenZipCodeIsCorrect_ItIsAcceptedAndFormattedCorrectly(string input, string expected)
    {
        var zipCode = ZipCodeDutch.Parse(input);
        Assert.Equal(expected, zipCode.Value);
        Assert.Equal(expected, zipCode.ToString());
    }
    [Theory]
    [InlineData("1234aaa")]
    [InlineData("1234a")]
    [InlineData("1234 a")]
    [InlineData("1234a1")]
    [InlineData("12341a")]
    [InlineData("0234 AA")]
    public void WhenZupCodeIsIncorrect_ItThrowsArgumentException(string input)
    {
        var exception = Assert.Throws<ArgumentException>(() => ZipCodeDutch.Parse(input));
        Assert.Contains(input, exception.Message);
    }
}