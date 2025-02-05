using HexMaster.DomainDrivenDesign.Abstractions;
using HexMaster.DomainDrivenDesign.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.DomainDrivenDesign.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }

}