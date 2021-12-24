using AlgoSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlgoSharp.Tests.ArrayTests;

public class LinearArraySum
{
    private AlgoAnalyzer analyzer;
    
    int Sum(CountableArray<int> array)
    {
        var total = analyzer.Get(0);
        for(var i = analyzer.Get(0); i < array.Count; i += 1)
        {
            total += array[i];
        }
        return total.Value;
    }
    [Fact]
    public void RecognizesLinear()
    {
        // Arrange
        analyzer = new AlgoAnalyzer();
        // Act
        var result = analyzer.Analyze(Sum);
        // Assert
        Assert.True(result.IsLinear());
    }
}
