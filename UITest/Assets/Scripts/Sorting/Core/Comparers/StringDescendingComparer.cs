public class StringDescendingComparer : Comparer
{
    public override int Compare(IComparable x, IComparable y)
    {
        return y.String.CompareTo(x.String);
    }
}
