namespace ValueObjects
{
    public class Notification
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public NotificationType Type { get; set; }
        public string Group { get; set; }
        public bool IsDeleted { get; set; }

        public Notification(string text, NotificationType type, string _group)
        {
            Text = text;
            Type = type;
            Group = _group;
        }

        public Notification()
        {
        }
    }
}