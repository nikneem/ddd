using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Reflection;
using HexMaster.DomainDrivenDesign.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.DomainDrivenDesign.DomainEvents;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, IEnumerable<Func<object, Task>>> HandlersCache = new();
    private static readonly MethodInfo? MakeDelegateMethod = typeof(DomainEventDispatcher).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic);
    private static readonly Type OpenGenericFuncType = typeof(Func<,>);
    private static readonly Type TaskType = typeof(Task);

    //public async Task Dispatch(IDomainEvent domainEvent)
    //{

    //    var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
    //    var handlers = serviceProvider.GetServices(handlerType);
    //    foreach (var handler in handlers)
    //    {
    //        if (handler is IDomainEventHandler domainEventHandler)
    //        {
    //            await domainEventHandler.Handle(domainEvent);
    //        }
    //    }
    //}

    public async Task Dispatch(IDomainEvent domainEvent)
    {

        if (MakeDelegateMethod == null)
        {
            throw new Exception("MakeDelegateMethod not found");
        }

        var handlers = HandlersCache.GetOrAdd(domainEvent.GetType(), eventType =>
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
                var makeDelegate = MakeDelegateMethod.MakeGenericMethod(eventType);
                var funcType = OpenGenericFuncType.MakeGenericType(eventType, TaskType);

                return serviceProvider
                    .GetServices(handlerType)
                    .Select(handler => handler
                        .GetType()
                        .GetMethod("Handle")
                        .CreateDelegate(funcType, handler))
                    .Select(handlerDelegateConcrete => (Func<object, Task>)makeDelegate.Invoke(null, [handlerDelegateConcrete]))
                    .ToList();
        });

        foreach (var eventHandler in handlers)
        {
            await eventHandler(domainEvent);
        }
    }

    public async Task Dispatch(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await Dispatch(domainEvent);
        }
    }

    private static Func<object, Task> MakeDelegate<T>(Func<T, Task> action)
        => value => action((T)value);

}