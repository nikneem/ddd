using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using HexMaster.DomainDrivenDesign.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.DomainDrivenDesign.DomainEvents;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task Dispatch(IDomainEvent domainEvent)
    {

        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        var handlers = serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is IDomainEventHandler domainEventHandler)
            {
                await domainEventHandler.Handle(domainEvent);
            }
        }
    }
    public async Task Dispatch(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await Dispatch(domainEvent);
        }
    }
}