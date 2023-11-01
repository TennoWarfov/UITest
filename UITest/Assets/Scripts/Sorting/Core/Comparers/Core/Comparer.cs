using System.Collections.Generic;

public abstract class Comparer : IComparer<IComparable>
{
    public abstract int Compare(IComparable x, IComparable y);
}
