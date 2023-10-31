using EventBusPattern;

public class StringSortEventSender : SortingEventSender
{
    protected override void SendSortingEvent(bool arg0)
    {
        base.SendSortingEvent(arg0);
        _eventBusHolder.EventBus.Raise(new StringSortEvent(arg0));
    }
}
