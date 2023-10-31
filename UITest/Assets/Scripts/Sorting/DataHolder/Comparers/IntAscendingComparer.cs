public class IntAscendingComparer : Comparer
{
    public override int Compare(IComparable x, IComparable y)
    {
        return x.Int.CompareTo(y.Int);
    }
}
