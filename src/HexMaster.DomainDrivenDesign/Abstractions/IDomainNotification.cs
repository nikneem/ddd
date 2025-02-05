using System;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainNotification
{
    Guid EventId { get; }
}