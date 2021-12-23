using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AlgoSharp.Types;

namespace AlgoSharp.Tests.CountingTests;

public class IntegerArrayCounting
{
    private readonly AlgoAnalyzer analyzer = new AlgoAnalyzer();
    private readonly Random random = new Random();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="numberOfOccurances"></param>
    /// <returns></returns>
    private AlgoArray<int> ComputeOccurances(AlgoArray<int> input)
    {
        var n = analyzer.Get(input.Count);
        var nextIndex = analyzer.Get(-1);
        var startIndex = analyzer.Get(0);
        while(startIndex < n || nextIndex != -1)
        {
            if(nextIndex == -1)
            {
                // Getting to this by default
                if (input[startIndex] < n)
                {
                    var value = input[startIndex];
                    input[startIndex] = n;
                    nextIndex = value;
                }
                startIndex++;
            }
            else if(input[nextIndex] > (n - 1))
            {
                // Been here before
                input[nextIndex]++;
                nextIndex = analyzer.Get(-1);
            }
            else
            {
                // Must be a new value
                var value = input[nextIndex];
                input[nextIndex] = n + 1;
                nextIndex = value;
            }
        }
        for(var i = analyzer.Get(0); i < n; i++)
        {
            input[i] = input[i] - n;
        }
        return input;
    }
    [Fact]
    public void CountsCorrectly()
    {
        analyzer.ResetCounts();

        var arr = new int[] { 3, 2, 3, 0, 3, 0, 1, 3};
        var input = analyzer.GetArray(arr);

        var result = ComputeOccurances(input);

        Assert.Equal(new int[] { 2, 1, 1, 4, 0, 0, 0, 0 }, result.ToArray());
    }

    [Fact]
    public void RecognizesLinear()
    {
        analyzer.ResetCounts();

        Func<int, object> inputGenerator = n => new AlgoArray<int>(
            Enumerable.Range(0, n).Select(i => random.Next(0, n)), 
            analyzer);

        var result = analyzer.Analyze(ComputeOccurances, inputGenerator);

        Assert.True(result.IsLinear(), "Expected the result to be linear.");
    }
}
