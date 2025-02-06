using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Linq;
using HexMaster.DomainDrivenDesign.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HexMaster.DomainDrivenDesign.DomainEvents;

public class DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger) : IDomainEventDispatcher
{
    public async Task Dispatch(IDomainEvent domainEvent)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        logger.LogInformation($"Dispatching event of type {domainEvent.GetType().Name} to handlers of type {handlerType.Name}");
        var handlers = serviceProvider.GetServices(handlerType).ToList();
        logger.LogInformation("Dispatching domain event to {count} handlers", handlers.Count);
        foreach (var handler in handlers)
        {
            if (handler is IDomainEventHandler domainEventHandler)
            {
                var sw = Stopwatch.StartNew();
                try
                {
                    logger.LogInformation("Executing handler for domain event {type}", domainEvent.GetType().Name);
                    await domainEventHandler.Handle(domainEvent);
                    logger.LogInformation("Handler executed in {ms} milliseconds", sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing handler for domain event {type}", domainEvent.GetType().Name);
                }
            }
        }
    }
    public async Task Dispatch(IEnumerable<IDomainEvent> domainEvents)
    {
        var enumerable = domainEvents as IDomainEvent[] ?? domainEvents.ToArray();
        if (enumerable.Any())
        {
            foreach (var domainEvent in enumerable)
            {
                await Dispatch(domainEvent);
            }
        }
    }
}