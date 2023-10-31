public class StringAscendingComparer : Comparer
{
    public override int Compare(IComparable x, IComparable y)
    {
        return x.String.CompareTo(y.String);
    }
}
