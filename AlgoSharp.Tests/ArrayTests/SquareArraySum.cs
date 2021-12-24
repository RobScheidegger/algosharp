using AlgoSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlgoSharp.Tests.ArrayTests;

public class SquareArraySum
{
    private AlgoAnalyzer analyzer;
    
    int Sum(CountableArray<int> array)
    {
        var total = analyzer.Get(0);
        for(var i = analyzer.Get(0); i < array.Count; i += 1)
        {
            for(var j = analyzer.Get(i.Value); j < array.Count; j += 1)
            {
                total += array[j - i];
            }
        }
        return total.Value;
    }
    [Fact]
    public void RecognizesSquare()
    {
        // Arrange
        analyzer = new AlgoAnalyzer();
        // Act
        var result = analyzer.Analyze(Sum);
        // Assert
        Assert.True(result.IsQuadratic(), "Expected the analysis to find quadratic runtime.");
    }
}
