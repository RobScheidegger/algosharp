using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AlgoSharp.Types;

namespace AlgoSharp.Tests.CountingTests;

public class IntegerArrayCountingTests
{
    private readonly AlgoAnalyzer analyzer;
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
                startIndex = startIndex + 1;
            }
            else if(input[nextIndex] > (n - 1))
            {
                // Been here before
                input[nextIndex] = input[nextIndex] + 1;
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
        for(var i = analyzer.Get(0); i < n; i = i + 1)
        {
            input[i] = input[i] - n;
        }
        return input;
    }
    [Fact]
    public void CountsDoubleCaseCorrectly()
    {
        var arr = new int[] { 3, 2, 3, 0, 3, 0, 1, 3};
        var input = analyzer.GetArray(arr);

        var result = ComputeOccurances(input);

        Assert.Single(result);
        Assert.Equal(3, result.First());
        analyzer.PrintResults();

    }
}
