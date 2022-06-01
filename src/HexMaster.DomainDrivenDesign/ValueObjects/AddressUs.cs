using System.Text;

namespace HexMaster.DomainDrivenDesign.ValueObjects
{
    public class AddressUs : Address
    {
        public string StateOrPrivince { get; private set; }

        public void SetStateOrProvince(string value)
        {
            StateOrPrivince = value;
        }

        public override string ToString(char splitCharacter)
        {
            var address = new StringBuilder(HouseNumber);
            address.Append(", ");
            address.Append(Street);
            address.Append(splitCharacter);
            address.Append(ZipCode);
            address.Append(splitCharacter);
            address.Append(City);
            address.Append(", ");
            address.Append(StateOrPrivince);
            address.Append(splitCharacter);
            address.Append(Country);
            return address.ToString();

        }
    }
}
