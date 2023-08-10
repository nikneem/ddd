using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace HexMaster.DomainDrivenDesign.Abstractions
{
    internal interface IDomainModel<TId>
    {
        TId Id { get; }
        TrackingState TrackingState { get; }
    }
}