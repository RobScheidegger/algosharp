using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Types;

public class CountableSet<T> where T : IComparable<T>
{
    private readonly HashSet<T> set = new();
    private readonly AlgoAnalyzer analyzer;

    public CountableSet(AlgoAnalyzer analyzer)
    {
        this.analyzer = analyzer;
    }

    public void Add(Countable<T> item)
    {
        analyzer.Count(1, "Set Add");
        set.Add(item.Value);
    }

    public bool Contains(Countable<T> item)
    {
        analyzer.Count(1, "Set Contains");
        return set.Contains(item.Value);
    }
}
