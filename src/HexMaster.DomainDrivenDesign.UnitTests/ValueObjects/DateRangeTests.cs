using HexMaster.DomainDrivenDesign.Exceptions;
using HexMaster.DomainDrivenDesign.ValueObjects;
using System;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.ValueObjects;

public class DateRangeTests
{

    [Fact]
    public void WhenNoEndDateSpecified_ThenDateRangeIsValid()
    {
        var expectedDate = DateTimeOffset.UtcNow;
        var dateRange = new OptionalDateRange(expectedDate);
        Assert.Equal(expectedDate, dateRange.StartDate);
    }
    [Fact]
    public void WhenValidDateRangeSpecified_ThenDateRangeIsValid()
    {
        var expectedStartDate = DateTimeOffset.UtcNow.AddDays(-7);
        var expectedEndDate = DateTimeOffset.UtcNow;
        var dateRange = new DateRange(expectedStartDate, expectedEndDate);
        Assert.Equal(expectedStartDate, dateRange.StartDate);
        Assert.Equal(expectedEndDate, dateRange.EndDate);
    }
    [Fact]
    public void WhenDateRangeIsInvalid_ItThrowsDomainException()
    {
        var expectedStartDate = DateTimeOffset.UtcNow.AddDays(7);
        var expectedEndDate = DateTimeOffset.UtcNow;
        var exception = Assert.Throws<DomainException>(() => new DateRange(expectedStartDate, expectedEndDate));
        Assert.Contains("End date of date range is before the start date", exception.Message);
    }
}