using HexMaster.DomainDrivenDesign.Events;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models.Events;

public class PersonFirstnameChangedEvent : PropertyChangedDomainEvent<string>
{
    internal PersonFirstnameChangedEvent(string oldValue, string newValue, string property = nameof(Person.Firstname))
    : base(oldValue, newValue, property)
    {
    }
}