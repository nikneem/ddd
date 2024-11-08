using HexMaster.DomainDrivenDesign.UnitTests.DomainModel.Models;
using System;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.DomainModel;

public class EntityIdTests
{

    [Fact]
    public void WhenDomainModelIsInstanciated_TheIdPropertyIsSet()
    {
        var expected = Guid.NewGuid();
        var domainModel = new DummyDomainModel(expected);
        Assert.Equal(domainModel.Id, expected);
    }

}