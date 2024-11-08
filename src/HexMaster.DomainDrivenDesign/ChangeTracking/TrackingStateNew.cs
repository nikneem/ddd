namespace HexMaster.DomainDrivenDesign.ChangeTracking;

public sealed class TrackingStateNew : TrackingState
{
    public override string Key => TrackingStateKey.New;
}