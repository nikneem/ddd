using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel
{
    public class TrackingStateSwitches
    {
        [Theory]
        [InlineData(TrackingStateKey.Pristine, TrackingStateKey.Touched)]
        [InlineData(TrackingStateKey.Pristine, TrackingStateKey.Deleted)]
        [InlineData(TrackingStateKey.Pristine, TrackingStateKey.Modified)]
        [InlineData(TrackingStateKey.Touched, TrackingStateKey.Modified)]
        [InlineData(TrackingStateKey.Touched, TrackingStateKey.Deleted)]
        public void WhenTrackingStateSwitchIsAllowed_ThenTheNewTrackingStateIsSet(string initialState, string endState)
        {
            var initialTrackingState = TrackingState.FromKey(initialState);
            var endTrackingState = TrackingState.FromKey(endState);

            var domainModel = new DummyDomainModel(TrackingState.Pristine);
            domainModel.ExposedSetState(initialTrackingState);
            domainModel.ExposedSetState(endTrackingState);
            Assert.Equal(domainModel.TrackingState, endTrackingState);
        }

        [Theory]
        [InlineData(TrackingStateKey.Pristine, TrackingStateKey.New)]
        [InlineData(TrackingStateKey.Touched, TrackingStateKey.New)]
        [InlineData(TrackingStateKey.Deleted, TrackingStateKey.New)]
        [InlineData(TrackingStateKey.Modified, TrackingStateKey.New)]
        [InlineData(TrackingStateKey.Modified, TrackingStateKey.Touched)]
        [InlineData(TrackingStateKey.Deleted, TrackingStateKey.Touched)]
        [InlineData(TrackingStateKey.Deleted, TrackingStateKey.Modified)]
        [InlineData(TrackingStateKey.Deleted, TrackingStateKey.Pristine)]
        public void WhenTrackingStateChangeIsDisallowed_TheInitialStateRemains(string initialState, string endState)
        {
            var initialTrackingState = TrackingState.FromKey(initialState);
            var endTrackingState = TrackingState.FromKey(endState);

            var domainModel = new DummyDomainModel(TrackingState.Pristine);
            domainModel.ExposedSetState(initialTrackingState);
            domainModel.ExposedSetState(endTrackingState);
            Assert.Equal(domainModel.TrackingState, initialTrackingState);
        }

        [Theory]
        [InlineData(TrackingStateKey.Pristine)]
        [InlineData(TrackingStateKey.Touched)]
        [InlineData(TrackingStateKey.Modified)]
        [InlineData(TrackingStateKey.Deleted)]
        public void WhenStateIsChangedToNew_ThenInitialStateRemains(string initialState)
        {
            var initialTrackingState = TrackingState.FromKey(initialState);

            var domainModel = new DummyDomainModel(TrackingState.Pristine);
            domainModel.ExposedSetState(initialTrackingState);
            domainModel.ExposedSetState(TrackingState.New);
            Assert.Equal(domainModel.TrackingState, initialTrackingState);
        }

    }
}
