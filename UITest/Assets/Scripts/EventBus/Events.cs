namespace EventBusPattern
{
    /*public readonly struct SortingEvent: IEvent
    {
        public readonly bool IntSort;
        public readonly bool StringSort;

        public SortingEvent(bool intSort, bool stringSort)
        {
            IntSort = intSort;
            StringSort = stringSort;
        }
    }*/

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