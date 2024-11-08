using System;
using HexMaster.DomainDrivenDesign.ValueObjects;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.ValueObjects;

public class IbanTests
{
    [Theory]
    [InlineData("NL16ABNA4228435912")]
    [InlineData("NL16 ABNA4228435912")]
    [InlineData("NL16 ABNA 4228435912")]
    [InlineData("NL16 ABNA 42 28 43 59 12")]
    public void WhenIbanIsValid_ItReturnsAnObject(string valid)
    {
        var iban =  Iban.FromNumber(valid);
        Assert.Equal(iban.Number, valid);
    }

    [Theory]
    [InlineData("BE16ABNA4228435912")]
    [InlineData("NL14 ABNA4228435912")]
    [InlineData("NL16 ABNX 4228435912")]
    [InlineData("NL16 ABNA 42 28 33 59 12")]
    public void WhenIbanIsInvalid_ThrowsArgumentException(string invalid)
    {
        var exception = Assert.Throws<ArgumentException>(() =>  Iban.FromNumber(invalid));
        Assert.True(exception.Message.Contains(invalid, StringComparison.InvariantCultureIgnoreCase));
    }
}