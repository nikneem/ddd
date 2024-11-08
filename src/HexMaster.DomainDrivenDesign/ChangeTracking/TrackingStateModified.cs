namespace HexMaster.DomainDrivenDesign.ChangeTracking;

public sealed class TrackingStateModified : TrackingState
{
    public override string Key => TrackingStateKey.Modified;
    public override bool CanSwitchTo(TrackingState newState)
    {
        return newState == Deleted;
    }
}