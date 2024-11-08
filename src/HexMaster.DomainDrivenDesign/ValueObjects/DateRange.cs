using System;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class DateRange
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }

    public DateRange(DateTimeOffset start, DateTimeOffset end)
    {
        if (start > end)
        {
            throw new DomainException(
                $"End date of date range is before the start date: Start '{start}', End '{end}'");
        }
        StartDate = start;
        EndDate = end;
    }
}