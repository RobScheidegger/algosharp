using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp;

/// <summary>
/// Contains extension methods aimed at making results more easily readable in console applications.
/// </summary>
public static class AnalysisConsoleExtensions
{
    /// <summary>
    /// Prints a detailed report of the resultant analysis of an algorithm to the console.
    /// </summary>
    /// <param name="analysis">The analysis (from an AlgoAnalyzer) to be printed.</param>
    public static void PrintDetailed(this AlgoAnalysis analysis, bool includeSubCategories = false)
    {
        Console.WriteLine("{0, 25}: {1, 10}", "Overall Runtime", analysis.Runtime);
        Console.WriteLine("{0, 25}: {1, 10}", "Confidence (0 to 1)", analysis.Confidence);
        Console.WriteLine("{0, 25}: {1, 10}", "Estimated Coefficient", analysis.Coefficient);
    }
}
