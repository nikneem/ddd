using System;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class OptionalDateRange : IComparable<OptionalDateRange>
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

    /// <summary>
    /// Compares this optional date range with another instance and returns true if the date values are different, false if they are the same.
    /// </summary>
    /// <param name="other">The other OptionalDateRange instance to compare with</param>
    /// <returns>True if the date ranges are different, false if they are the same</returns>
    public bool Compare(OptionalDateRange other)
    {
        if (other == null)
            return true;

        return StartDate != other.StartDate || EndDate != other.EndDate;
    }

    /// <summary>
    /// Compares the current instance with another OptionalDateRange and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">The OptionalDateRange to compare with this instance</param>
    /// <returns>A value that indicates the relative order of the objects being compared</returns>
    public int CompareTo(OptionalDateRange other)
    {
        if (other == null)
            return 1;

        // First compare by StartDate
        var startComparison = StartDate.CompareTo(other.StartDate);
        if (startComparison != 0)
            return startComparison;

        // If StartDate is equal, compare by EndDate
        // Handle nullable EndDate comparison
        if (EndDate == null && other.EndDate == null)
            return 0;
        if (EndDate == null)
            return -1; // null EndDate is considered "less than" a value
        if (other.EndDate == null)
            return 1;

        return EndDate.Value.CompareTo(other.EndDate.Value);
    }
}
