using AlgoSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlgoSharp.Tests.ArrayTests;

public class BinarySearchTests
{
    private AlgoAnalyzer analyzer = new();

    /// <summary>
    /// Determines whether or not a sorted array contains a given key.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Contains(AlgoArray<int> array, AlgoStruct<int> key)
    {
        var min = analyzer.Get(0);
        var max = analyzer.Get(array.Count) - 1;
        while (min <= max)
        {
            var mid = (min + max) / 2;
            if (key == array[mid])
            {
                return true;
            }
            else if (key < array[mid])
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }
        return false;
    }

    [Fact]
    public void RecognizesLog()
    {
        var key = analyzer.Get(9);
        Func<AlgoArray<int>, bool> func = (a) => Contains(a, key);
        Func<int, object> generator = n => new AlgoArray<int>(
            Enumerable.Range(0, n).Select(i => Random.Shared.Next()).OrderBy(i => i),
            analyzer);

        var result = analyzer.Analyze(func, generator);

        Assert.True(result.IsLogarithmic(), "Expect the analysis to be logarithmic time.");
    }
}
