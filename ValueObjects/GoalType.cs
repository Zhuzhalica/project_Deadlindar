using System.Drawing;

namespace ValueObjects
{
    public class GoalType
    {
        public string Title { get; set; }
        public ColorARGB Color { get; set; }

        public GoalType(string title, ColorARGB color)
        {
            this.Title = title;
            this.Color = color;
        }
    }
}