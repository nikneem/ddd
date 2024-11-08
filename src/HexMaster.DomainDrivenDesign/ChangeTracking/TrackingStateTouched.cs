namespace HexMaster.DomainDrivenDesign.ChangeTracking;

public sealed class TrackingStateTouched : TrackingState
{
    public override string Key => TrackingStateKey.Touched;
    public override bool CanSwitchTo(TrackingState newState)
    {
        return newState == Deleted || newState == Modified;

    }
}