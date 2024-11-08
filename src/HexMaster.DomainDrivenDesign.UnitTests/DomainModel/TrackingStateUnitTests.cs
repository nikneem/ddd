using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.Exceptions;
using HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel;

public class TrackingStateUnitTests
{

    [Theory]
    [InlineData(TrackingStateKey.New)]
    [InlineData(TrackingStateKey.Pristine)]
    public void WhenInitialStateIsValid_ThenTheDomainModelIsInstanciated(string trackingStateKey)
    {
        var trackingState = TrackingState.FromKey(trackingStateKey);
        var model = new DummyDomainModel(trackingState);
        Assert.NotNull(model);
    }

    [Theory]
    [InlineData(TrackingStateKey.Touched)]
    [InlineData(TrackingStateKey.Modified)]
    [InlineData(TrackingStateKey.Deleted)]
    public void WhenInitialStateIsInvalid_ItThrowsDomainException(string trackingStateKey)
    {
        var trackingState = TrackingState.FromKey(trackingStateKey);
        var exception = Assert.Throws<DomainException>(() =>  new DummyDomainModel(trackingState));
        Assert.Contains("The initial state of a domain model", exception.Message);
    }

}