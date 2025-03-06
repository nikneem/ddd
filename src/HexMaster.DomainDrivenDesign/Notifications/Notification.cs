using System.Collections.Generic;
using System.Linq;

namespace HexMaster.DomainDrivenDesign.Notifications;

public sealed class ValidationNotification
{
    private readonly List<string> _errors = [];

    public void AddError(string message) => _errors.Add(message);

    public bool HasErrors() => _errors.Any();

    public IReadOnlyList<string> GetErrors() => _errors.AsReadOnly();
}