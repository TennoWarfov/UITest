public class IntDescendingComparer : Comparer
{
    public override int Compare(IComparable x, IComparable y)
    {
        return y.Int.CompareTo(x.Int);
    }
}
