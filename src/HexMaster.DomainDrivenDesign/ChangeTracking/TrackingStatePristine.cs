namespace HexMaster.DomainDrivenDesign.ChangeTracking
{
    public sealed class TrackingStatePristine : TrackingState
    {
        public override string Key => TrackingStateKey.Pristine;
        public override bool CanSwitchTo(TrackingState newState)
        {
            return newState == Deleted || newState == Modified || newState == Touched;
        }
    }
}
