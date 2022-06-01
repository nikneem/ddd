using HexMaster.DomainDrivenDesign.ValueObjects;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void WhenDutchAddressIsInputted_ItIsParsedAsValidDutchAddress()
        {
            var expected = "Straatlaanseweg 23\n2233 XL\nScharweijde Noord\nNederland";
            var address = new AddressDutch();
            address.SetStreet("Straatlaanseweg");
            address.SetHouseNumber("23");
            address.SetZipCode(ZipCodeDutch.Parse("2233xl"));
            address.SetCity("Scharweijde Noord");
            address.SetCountry("Nederland");
            Assert.Equal(expected, address.ToString());
        }
        [Fact]
        public void WhenUsAddressIsInputted_ItIsParsedAsValidDutchAddress()
        {
            var expected = "23, Straatlaanseweg\n2233 XL\nScharweijde Noord, CA\nNederland";
            var address = new AddressUs();
            address.SetStreet("Straatlaanseweg");
            address.SetHouseNumber("23");
            address.SetStateOrProvince("CA");
            address.SetZipCode(ZipCodeDutch.Parse("2233xl"));
            address.SetCity("Scharweijde Noord");
            address.SetCountry("Nederland");
            Assert.Equal(expected, address.ToString());
        }



        [Fact]
        public void WhenStreetAndHouseNumberAreSet_TheValuesAreReturned()
        {
            var expectedStreet = "Kerklaan";
            var expectedNumber = "23";
            var address = new AddressDutch();
            address.SetStreet(expectedStreet);
            address.SetHouseNumber(expectedNumber);
            Assert.Equal(expectedStreet,address.Street);
            Assert.Equal(expectedNumber, address.HouseNumber);
        }

        [Fact]
        public void WhenAddressZipCodeIsSet_ItReturnsTheNewZipCode()
        {
            var inputZipCode = "2581ab";
            var expectedZipCode = "2581 AB";
            var address = new AddressDutch();
            address.SetZipCode(ZipCodeDutch.Parse(inputZipCode));
            Assert.Equal(expectedZipCode, address.ZipCode.Value);
        }

    }
}
