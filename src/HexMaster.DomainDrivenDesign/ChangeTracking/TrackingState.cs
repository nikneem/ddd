namespace HexMaster.DomainDrivenDesign.ChangeTracking
{
    public abstract class TrackingState
    {

        public static TrackingState New;
        public static TrackingState Pristine;
        public static TrackingState Touched;
        public static TrackingState Modified;
        public static TrackingState Deleted;

        public static TrackingState[] All;

        public abstract string Key { get; }
        public virtual bool CanSwitchTo(TrackingState newState)
        {
            return false;
        }

        static TrackingState()
        {
            All = new[]
            {
                New = new TrackingStateNew(),
                Pristine = new TrackingStatePristine(),
                Touched = new TrackingStateTouched(),
                Modified = new TrackingStateModified(),
                Deleted = new TrackingStateDeleted()
            };
        }

    }
}
