using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

/// <summary>
/// Gives the detailed results of a single trial with a given parameter size.
/// </summary>
public class RuntimeResult
{
    /// <summary>
    /// The detailed runtime given by category.
    /// </summary>
    public Dictionary<string, int> Details { get; set; }
    /// <summary>
    /// The sizes of the input parameters.
    /// </summary>
    public int[] ParameterValues { get; set; }
    /// <summary>
    /// Total runtime (sum of the values in details).
    /// </summary>
    public int Total { get; set; }

    public override string ToString()
    {
        return $"Total: {Total}, Values: '{string.Join(',', ParameterValues)}'";
    }
}
