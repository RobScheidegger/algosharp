# AlgoSharp

As someone who really enjoys both the C# language and the study of algorithms and computational theory, I wanted an interesting tool to be able to better explain what *actually* happens when an algorithm is executed, and in a scientific manner, perform some tests to experimentally verify that our theory is indeed correct.

This project arose out of an Algorithms class I took in Fall 2021. Although this sort of analysis should serve impractical for larger, more complex algorithms on the forefront of research, it aims to serve as a useful learning and educational tool nonetheless.

## Features

## Current Limitations

As of now, the following are a few limitations of the current library. They may be eventually remedied (and in fact some may already be partially implemented).

1. 

## How to Perform Analysis

To perform a runtime analysis of a specific function, 

1. Create a new `AlgoAnalyzer` class:
    ```
    var analyzer = new AlgoAnalyzer();
    ```
2. Create a method or function that you want to test. This method should:

    a. Have only variable-size inputs (aka Arrays, Lists, etc.). If a function does not (ex. binary search on an array that takes a key), simply specify a pre-determined key beforehand and curry the function with a new `Func` or lambda expression.
    b. Only have inputs as `Countable` (including any inputs that ), otherwise operations executed by these variables will not be counted.
    c. Only use *variables* that are countable and attached to the `AlgoAnalyzer`. The easiest way to do this would be to use the `AlgoAnalyzer.Get` method on the analyzer (which can give a `Countable<T>`) for any literal that you can input. 

3. Call `AlgoAnalyzer.Analyze` and pass in the method or lambda of the algorithm as a parameter. Optionally, pass in any custom input generators that this algorithm requires (default ones will try to be provided), 

## Example

The following is an example of analyzing the runtime differences of a naive unique element algorithm and a more intelligent one using hash sets. This is given here as an example, but can be found and executed in `AlgoSharp.Example/Program.cs`.

First, we perform a naive algorithm which just iterates through and compares each element of the list (expected `O(n^2)`) runtime:


```C#
using AlgoSharp;
using AlgoSharp.Types;

var analyzer = new AlgoAnalyzer();
/// <summary>
/// Naively Determines whether or not a list of integers has unique values.
/// </summary>
bool IsUniqueNaive(CountableArray<int> array)
{
    for(var i = analyzer.Get(0); i < array.Count; i++)
    {
        for(var j = analyzer.Get(0); j < array.Count; j++)
        {
            if (i == j) continue; // Same index

            if (array[i] == array[j]) return false;
        }
    }
    return true;
}

var naiveAnalysis = analyzer.Analyze(IsUniqueNaive);

naiveAnalysis.PrintDetailed();
```

Which prints the expected result:

```
          Overall Runtime:     O(n^2)
      Confidence (0 to 1):          1
    Estimated Coefficient:          6
```

We can then, in the same file try to analyze a better (rational) implementation using hash sets, and analyze it as well:

```C#
/// <summary>
/// Determines whether or not a list of integers has unique values (using a HashMap/Dictionary).
/// </summary>
bool IsUnique(CountableArray<int> array)
{
    var set = analyzer.GetSet<int>();

    for(var i = analyzer.Get(0); i < array.Count; i++)
    {
        if (set.Contains(array[i]))
            return false;
        set.Add(array[i]);
    }

    return true;
}

var analysis = analyzer.Analyze(IsUnique);

analysis.PrintDetailed();
```

Which now shows us:

```
          Overall Runtime:       O(n)
      Confidence (0 to 1):          1
    Estimated Coefficient:          6
```

Showing that this works nicely in distinguishing between two implementations of the same algorithm with different asymptotic complexities.