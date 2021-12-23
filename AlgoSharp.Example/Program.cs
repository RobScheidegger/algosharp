using AlgoSharp;
using AlgoSharp.Types;

var analyzer = new AlgoAnalyzer();
/// <summary>
/// Naively Determines whether or not a list of integers has unique values.
/// </summary>
bool IsUniqueNaive(AlgoArray<int> array)
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

var analysis = analyzer.Analyze(IsUniqueNaive);

analysis.PrintDetailed();