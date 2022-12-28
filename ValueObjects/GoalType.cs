using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace ValueObjects
{
    [Owned]
    public class GoalType
    {
        public string Title { get; set; }
        public ColorARGB Color { get; set; }

        public GoalType(string title, ColorARGB color)
        {
            this.Title = title;
            this.Color = color;
        }
        
        public GoalType()
        {
            this.Title = "";
            this.Color = new ColorARGB(1,0,0,0);
        }
        
        public void ChangeColor(ColorARGB color)
        {
            Color = color;
        }
        
        public void ChangeTitle(string title)
        {
            this.Title = title;
        }

        public override bool Equals(object? obj)
        {
            if (obj is GoalType c)
            {
                return c.Color == Color && c.Title == Title;
            }

            return false;
        }
        public static bool operator ==(GoalType r1, GoalType r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(GoalType r1, GoalType r2)
        {
            return !(r1 == r2);
        }
    }
}