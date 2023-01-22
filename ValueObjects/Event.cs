using System;
using System.Drawing;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    public class Event
    {
        public TimeInterval TimeInterval { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GoalType GoalType { get; set; }
        public bool IsDeleted { get; set; }

        public string Group = "Личные";

        public Event(string title, GoalType goalType, TimeInterval timeInterval, string description)
        {
            this.Title = title;
            this.Description = description;
            this.GoalType = goalType;
            this.TimeInterval = timeInterval;
        }

        public Event()
        {
            this.Title = "";
            this.Description = "";
            this.GoalType = new GoalType();
            this.TimeInterval = new TimeInterval();
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
            this.TimeInterval = new TimeInterval() {startTime = startTime, endTime = endTime};
        }

        public void ChangeType(GoalType type)
        {
            this.GoalType = type;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Event ev)
            {
                return ev.IsDeleted == this.IsDeleted && ev.Description == this.Description && ev.Title == this.Title &&
                       ev.GoalType == this.GoalType && ev.TimeInterval == this.TimeInterval;
            }

            return false;
        }

        public static bool operator ==(Event r1, Event r2)
        {
            try
            {
                return r1.Equals(r2);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator !=(Event r1, Event r2)
        {
            return !(r1 == r2);
        }
    }
}