namespace HexMaster.DomainDrivenDesign.ChangeTracking
{
    public sealed class TrackingStateDeleted : TrackingState
    {
        public override string Key => TrackingStateKey.Deleted;
    }
}
