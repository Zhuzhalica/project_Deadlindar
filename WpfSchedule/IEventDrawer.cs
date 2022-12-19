using ValueObjects;

namespace WpfSchedule
{
    public interface IEventDrawer
    {
        public Event Event { get; }
        public void DrawEvent();
    }
}