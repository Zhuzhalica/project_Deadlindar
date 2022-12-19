using System;
using System.Drawing;

namespace ValueObjects
{
    public class Event
    {
        public TimeInterval TimeInterval { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GoalType GoalType { get; set; }

        public Event(string title, GoalType goalType, TimeInterval timeInterval, string description = "")
        {
            this.Title = title;
            this.Description = description;
            this.GoalType = goalType;
            this.TimeInterval = timeInterval;
        }

        public Event()
        {
        }

        public void ChangeTitle(string title)
        {
            this.Title = title;
        }

        public void ChangeDescription(string description)
        {
            this.Description = description;
        }

        public void ResetTime(DateTime startTime, DateTime endTime)
        {
            this.TimeInterval = new TimeInterval(startTime, endTime);
        }

        public void ChangeType(GoalType type)
        {
            this.GoalType = type;
        }
    }
}