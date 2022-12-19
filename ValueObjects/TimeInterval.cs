using System;

namespace ValueObjects
{
    public class TimeInterval
    {
        public DateTime startTime { get; }
        public DateTime endTime { get; }

        public TimeInterval(DateTime startTime, DateTime endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public TimeInterval ChangeStartTime(DateTime startTime)
        {
            return new TimeInterval(startTime, endTime);
        }
        
        public TimeInterval ChangeEndTime(DateTime endTime)
        {
            return new TimeInterval(startTime, endTime);
        }
    }
}