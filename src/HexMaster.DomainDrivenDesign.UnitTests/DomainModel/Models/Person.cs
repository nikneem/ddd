using System;
using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models.Events;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models;

public class Person : AggregateRoot<Guid>
{
    public string Firstname { get; private set; }
    public string Lastname { get; }
    public string EmailAddress { get; }
    public DateTime DateOfBirth { get; }

    public int Age
    {
        get
        {
            var age = DateTime.Now.Year - DateOfBirth.Year;
            return DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? -age : age;
        }
    }

    public void SetFirstname(string value)
    {
        if (!Equals(Firstname, value))
        {
            AddDomainEvent(new PersonFirstnameChangedEvent(Firstname, value));
            Firstname = value;
            SetState(TrackingState.Modified);
        }
    }

    public Person(Guid id, string firstname, string lastname, string emailAddress, DateTime dateOfBirth) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        EmailAddress = emailAddress;
        DateOfBirth = dateOfBirth;
    }
}