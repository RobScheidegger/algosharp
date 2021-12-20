using AlgoSharp.AlgoTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlgoSharp.Tests.ArrayTests;

public class BasicArrayOperationsTests
{
    private AlgoAnalyzer analyzer;
    // Arrange
    [InputConfiguration]
    int Sum(AlgoArray<int> array)
    {
        var total = analyzer.Get(0);
        for(var i = analyzer.Get(0); i < array.Count; i = i + 1)
        {
            total = total + array[i];
        }
        return total.Value;
    }
    [Fact]
    public void CountsArrayGet()
    {
        analyzer = new AlgoAnalyzer(Sum);
        var result = analyzer.Analyze();
        Assert.True(result.IsLinear());
    }
}
