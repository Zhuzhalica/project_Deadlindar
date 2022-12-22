using ValueObjects;

namespace WpfLibrary
{
    public interface IEventDrawer
    {
        public Event Event { get; }
        public void DrawEvent();
    }
}