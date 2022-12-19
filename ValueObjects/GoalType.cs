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
        
        public void ChangeColor(ColorARGB color)
        {
            Color = color;
        }
        
        public void ChangeTitle(string title)
        {
            this.Title = title;
        }
    }
}