# Domain Driven Design Support Library

Welcome to the **Domain Driven Design Support Library**, a NuGet package designed to simplify the implementation of Domain-Driven Design (DDD) in your projects. This library abstracts away the base plumbing required for DDD, allowing you to focus on your core domain logic.

## Key Features

- Provides the essential `IDomainModel` interface to define domain models.
- Includes a base implementation `DomainModel<T>` to streamline common operations for domain models.
- Supports domain events with built-in event handling and dispatching mechanisms.
- Helps you adhere to DDD principles while reducing boilerplate code.
- Highly extensible and ready to integrate into existing DDD-based architectures.

## Getting Started

### Installation

Add the NuGet package to your project:

```bash
Install-Package HexMaster.DomainDrivenDesign
```

### Usage

#### 1. Implementing Domain Models

The core of this library is the `IDomainModel` interface. It provides the foundation for all domain models in your application. 

For most use cases, you can inherit from the `DomainModel<T>` base class, which already implements `IDomainModel` and provides useful base functionality.

#### Example

Here's an example of creating a domain model for an `Order` entity:

```csharp
using HexMaster.DomainDrivenDesign;

public class Order : DomainModel<Guid>
{
    public string CustomerName { get; private set; }
    public DateTime OrderDate { get; private set; }

    public Order(Guid id, string customerName, DateTime orderDate) : base(id)
    {
        CustomerName = customerName;
        OrderDate = orderDate;
    }

    public void UpdateCustomerName(string newCustomerName)
    {
        if (string.IsNullOrWhiteSpace(newCustomerName))
            throw new ArgumentException("Customer name cannot be empty.");

        CustomerName = newCustomerName;
    }
}
```

#### 2. Base Class Benefits

The `DomainModel<T>` base class simplifies your domain model by:

- Enforcing an ID property of type `T`.
- Ensuring equality checks based on the ID.
- Abstracting away common domain logic.

#### Example Base Class Functionality

```csharp
Order order1 = new Order(Guid.NewGuid(), "John Doe", DateTime.UtcNow);
Order order2 = new Order(order1.Id, "John Doe", DateTime.UtcNow);

// Equality check based on ID
Console.WriteLine(order1 == order2); // True
```

## Domain Events

Domain events are a key concept in Domain-Driven Design, allowing you to capture and react to significant changes or events within your domain. This library provides built-in support for domain events, enabling easy integration and handling.

### IDomainEvent Interface

All domain events in this library must implement the `IDomainEvent` interface. This interface represents an event that has occurred within your domain.

```csharp
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
```

### Raising Domain Events

The `DomainModel<T>` base class includes a method `AddDomainEvent` for raising domain events. These events can be processed later by the Domain Event Dispatcher.

#### Example

```csharp
using HexMaster.DomainDrivenDesign;

public class Order : DomainModel<Guid>
{
    public string CustomerName { get; private set; }

    public Order(Guid id, string customerName) : base(id)
    {
        CustomerName = customerName;
    }

    public void PlaceOrder()
    {
        // Raise a domain event
        AddDomainEvent(new OrderPlacedEvent(Id));
    }
}

public class OrderPlacedEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public OrderPlacedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
```

### Domain Event Dispatcher

The Domain Event Dispatcher processes and dispatches domain events to their corresponding handlers. It integrates with Dependency Injection (DI) to resolve and invoke event handlers automatically.

#### Enabling the Dispatcher

To use the Domain Event Dispatcher, register it in your application startup:

```csharp
services.AddDomainEventDispatcher();
```

### Handling Domain Events

Event handlers process domain events when they are dispatched. Handlers must inherit from the `DomainEventHandler<TEvent>` base class, where `TEvent` is the type of domain event to handle.

The `DomainEventHandler<TEvent>` base class provides an abstract `Handle` method that you override to define your handling logic.

#### Example

```csharp
using DDD.Support.Library;
using System.Threading.Tasks;

public class OrderPlacedEventHandler : DomainEventHandler<OrderPlacedEvent>
{
    public override Task Handle(OrderPlacedEvent domainEvent)
    {
        // Handle the event (e.g., send an email, update a database, etc.)
        Console.WriteLine($"Order placed with ID: {domainEvent.OrderId}");
        return Task.CompletedTask;
    }
}
```

### Complete Workflow

1. Raise a domain event using `AddDomainEvent` in your domain model.
2. Register the Domain Event Dispatcher in your application startup.
3. Implement event handlers by inheriting from `DomainEventHandler<TEvent>` and overriding the `Handle` method.
4. When the domain event is raised, the dispatcher will resolve the appropriate handler(s) and invoke the `Handle` method.

## Interfaces and Classes

### IDomainModel

The `IDomainModel` interface is the contract for all domain models in your application. It ensures that all domain models have an ID and enforce a basic equality structure.

```csharp
public interface IDomainModel
{
    object Id { get; }
}
```

### DomainModel<T>

The `DomainModel<T>` class provides a base implementation of `IDomainModel` and includes:

- A strongly-typed ID property.
- Overridden equality methods (`Equals`, `GetHashCode`, `==`, `!=`).
- A constructor that enforces the initialization of the ID.
- Support for raising domain events through `AddDomainEvent`.

```csharp
public abstract class DomainModel<T> : IDomainModel
{
    public T Id { get; }

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected DomainModel(T id)
    {
        if (id == null || id.Equals(default(T)))
            throw new ArgumentException("ID cannot be null or default.");

        Id = id;
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object obj)
    {
        if (obj is DomainModel<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        return false;
    }

    public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Id);

    public static bool operator ==(DomainModel<T> a, DomainModel<T> b) =>
        a?.Equals(b) ?? b is null;

    public static bool operator !=(DomainModel<T> a, DomainModel<T> b) => !(a == b);

    object IDomainModel.Id => Id;
}
```

## Extending the Library

If the `DomainModel<T>` class does not meet your specific requirements, you can:

1. Implement the `IDomainModel` interface directly.
2. Extend `DomainModel<T>` with additional functionality.

## Contributing

Contributions are welcome! Feel free to submit issues, feature requests, or pull requests on the [GitHub repository](https://github.com/your-repo-url-here).

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Feedback and Support

If you encounter any issues or have suggestions, please open an issue on GitHub or contact the maintainers directly.

---

Start building your next Domain-Driven Design project with confidence using the **Domain Driven Design Support Library**!

