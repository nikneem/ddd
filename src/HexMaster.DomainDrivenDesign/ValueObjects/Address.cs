using HexMaster.DomainDrivenDesign.Abstractions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public abstract class Address : IAddress
{
    protected char SplitCharacter { get; }

    public string Street { get; private set; }
    public string HouseNumber { get; private set; }
    public string City { get; private set; }
    public IZipCode ZipCode { get; private set; }
    public string Country { get; private set; }

    public virtual void SetStreet(string value)
    {
        Street = value;
    }

    public virtual void SetHouseNumber(string value)
    {
        HouseNumber = value;
    }

    public virtual void SetCity(string value)
    {
        City = value;
    }

    public virtual void SetZipCode(IZipCode value)
    {
        ZipCode = value;
    }

    public  virtual void SetCountry(string value)
    {
        Country = value;
    }

    public override string ToString()
    {
        return ToString(SplitCharacter);
    }
    public abstract string ToString(char splitCharacter);

    public Address(string street, string city, string houseNumber, IZipCode zipCode, string country)
        : this()
    {
        Street = street;
        City = city;
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        Country = country;
    }
    public Address()
    {
        SplitCharacter = '\n';
    }
}