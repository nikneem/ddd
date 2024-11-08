namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IAddress
{
    string Street { get;  }
    string HouseNumber { get;  }
    string City { get; }
    IZipCode ZipCode { get; }
    string Country { get; }

}