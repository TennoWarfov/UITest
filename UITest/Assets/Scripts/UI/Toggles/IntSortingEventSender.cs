using EventBusPattern;

public class IntSortingEventSender : SortingEventSender
{
    protected override void SendSortingEvent(bool arg0)
    {
        base.SendSortingEvent(arg0);
        _eventBusHolder.EventBus.Raise(new IntSortEvent(arg0));
    }
}