using System;
using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    [Owned]
    public class TimeInterval
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        // public TimeInterval(DateTime startTime, DateTime endTime)
        // {
        //     this.startTime = startTime;
        //     this.endTime = endTime;
        // }
        //
        // public TimeInterval()
        // {
        //     this.startTime = DateTime.MinValue;
        //     this.endTime = DateTime.Now;
        // }
        // public TimeInterval ChangeStartTime(DateTime startTime)
        // {
        //     return new TimeInterval(startTime, endTime);
        // }
        //
        // public TimeInterval ChangeEndTime(DateTime endTime)
        // {
        //     return new TimeInterval(startTime, endTime);
        // }
    }
}