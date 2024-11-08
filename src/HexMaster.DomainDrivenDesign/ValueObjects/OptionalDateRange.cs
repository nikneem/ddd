using System;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class OptionalDateRange
{
    public DateTimeOffset StartDate { get; } 
    public DateTimeOffset? EndDate { get; } 

    public OptionalDateRange(DateTimeOffset start, DateTimeOffset? end = null)
    {
        if (end.HasValue)
        {
            if (start > end.Value)
            {
                throw new DomainException(
                    $"End date of date range is before the start date: Start '{start}', End '{end}'");
            }
        }
        StartDate = start;
        EndDate = end;
    }

}
    
