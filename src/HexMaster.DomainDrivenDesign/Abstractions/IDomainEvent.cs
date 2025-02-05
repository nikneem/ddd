using System;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainEvent
{
    Guid EventId { get; }
}