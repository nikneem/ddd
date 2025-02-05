using HexMaster.DomainDrivenDesign.ChangeTracking;
using System;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models;

internal class DummyDomainModel : DomainModel<Guid>
{
    public DummyDomainModel(TrackingState state) : base(Guid.NewGuid(), state)
    {
    }
    public DummyDomainModel(Guid id) : base(id, TrackingState.Pristine)
    {
    }

    public void ExposedSetState(TrackingState state)
    {
        SetState(state);
    }
}