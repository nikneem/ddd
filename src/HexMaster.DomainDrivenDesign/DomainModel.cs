using HexMaster.DomainDrivenDesign.Abstractions;
using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign
{

    public abstract class DomainModel<TId> : IDomainModel<TId>
    {

        public TId Id { get; }

        public TrackingState TrackingState { get; private set; }

        protected void SetState(TrackingState state)
        {
            if (TrackingState.CanSwitchTo(state))
            {
                TrackingState = state;
            }
        }

        protected DomainModel(TId id, TrackingState state = null)
        {
            var initialState = state ?? TrackingState.Pristine;
            if (initialState != TrackingState.New && initialState != TrackingState.Pristine)
            {
                throw new DomainException(
                    $"The initial state of a domain model must always be New or Pristine. The current value '{initialState.Key}' is not allowed.");
            }

            Id = id;
            TrackingState = initialState;
        }
    }
}