using System.Collections.Generic;

namespace EventBusPattern
{
    public readonly struct DataLoadEvent : IEvent
    {
        public readonly List<Data> Datas;

        public DataLoadEvent(List<Data> datas)
        {
            Datas = datas;
        }
    }

    public readonly struct IntSortEvent : IEvent
    {
        public readonly bool Value;

        public IntSortEvent(bool value)
        {
            Value = value;
        }
    }

    public readonly struct StringSortEvent : IEvent
    {
        public readonly bool Value;

        public StringSortEvent(bool value)
        {
            Value = value;
        }
    }
}