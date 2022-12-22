using System;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    public class Event
    {
        public TimeInterval TimeInterval { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GoalType GoalType { get; set; }

        // public Event(string title, GoalType goalType, TimeInterval timeInterval, string description)
        // {
        //     this.Title = title;
        //     this.Description = description;
        //     this.GoalType = goalType;
        //     this.TimeInterval = timeInterval;
        // }
        //
        // public Event()
        // {
        //     this.Title = "";
        //     this.Description = "";
        //     this.GoalType = new GoalType();
        //     this.TimeInterval = new TimeInterval();
        // }

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
            this.TimeInterval = new TimeInterval(){startTime = startTime, endTime = endTime};
        }

        public void ChangeType(GoalType type)
        {
            this.GoalType = type;
        }
    }
}