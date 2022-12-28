using System;
using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    [Owned]
    public class TimeInterval
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public TimeInterval(DateTime startTime, DateTime endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
        
        public TimeInterval()
        {
            this.startTime = DateTime.Now;
            this.endTime = DateTime.Now;
        }
        public TimeInterval ChangeStartTime(DateTime startTime)
        {
            return new TimeInterval(startTime, endTime);
        }
        
        public TimeInterval ChangeEndTime(DateTime endTime)
        {
            return new TimeInterval(startTime, endTime);
        }

        public override bool Equals(object? obj)
        {
            if (obj is TimeInterval tv)
            {
                return tv.endTime == endTime && tv.startTime == startTime;
            }

            return false;
        }
        
        public static bool operator ==(TimeInterval r1, TimeInterval r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(TimeInterval r1, TimeInterval r2)
        {
            return !(r1 == r2);
        }
    }
}