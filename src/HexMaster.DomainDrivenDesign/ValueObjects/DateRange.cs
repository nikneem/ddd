using System;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign.ValueObjects;

public class DateRange : IComparable<DateRange>
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

    /// <summary>
    /// Compares this date range with another instance and returns true if the date values are different, false if they are the same.
    /// </summary>
    /// <param name="other">The other DateRange instance to compare with</param>
    /// <returns>True if the date ranges are different, false if they are the same</returns>
    public bool Compare(DateRange other)
    {
        if (other == null)
            return true;

        return StartDate != other.StartDate || EndDate != other.EndDate;
    }

    /// <summary>
    /// Compares the current instance with another DateRange and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">The DateRange to compare with this instance</param>
    /// <returns>A value that indicates the relative order of the objects being compared</returns>
    public int CompareTo(DateRange other)
    {
        if (other == null)
            return 1;

        // First compare by StartDate
        var startComparison = StartDate.CompareTo(other.StartDate);
        if (startComparison != 0)
            return startComparison;

        // If StartDate is equal, compare by EndDate
        return EndDate.CompareTo(other.EndDate);
    }
}