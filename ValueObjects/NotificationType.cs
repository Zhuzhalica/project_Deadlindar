namespace ValueObjects
{
    public class NotificationType
    {
        public string Title { get; set; }
        public ColorARGB Color { get; set; }

        public NotificationType(string title, ColorARGB color)
        {
            Title = title;
            Color = color;
        }
    }
}