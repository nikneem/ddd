using System;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign.ValueObjects
{
    public class DateRange
    {
        public DateTimeOffset StartDate { get;  }
        public DateTimeOffset? EndDate { get;  }

        public DateRange(DateTimeOffset start, DateTimeOffset? end = null)
        {
            if (end.HasValue)
            {
                if (start > end.Value)
                {
                    throw new DomainException(
                        $"End date of date range is before the start date: Start '{start}', End '{end}'");
                }
            }
        }
    }
}