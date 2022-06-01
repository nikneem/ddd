using System.Text;

namespace HexMaster.DomainDrivenDesign.ValueObjects
{
    public class AddressDutch : Address
    {
        public override string ToString(char splitCharacter)
        {
            var address = new StringBuilder(Street);
            address.Append(" ");
            address.Append(HouseNumber);
            address.Append(splitCharacter);
            address.Append(ZipCode);
            address.Append(splitCharacter);
            address.Append(City);
            address.Append(splitCharacter);
            address.Append(Country);
            return address.ToString();
        }
    }
}
