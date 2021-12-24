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