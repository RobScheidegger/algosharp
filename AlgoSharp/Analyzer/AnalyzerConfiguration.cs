using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

/// <summary>
/// Defines the configuration for the AlgoAnalyzer.
/// </summary>
public class AnalyzerConfiguration
{
    /// <summary>
    /// Optional verifier for the algorithm. Will run on every run if specified.
    /// The first argument is an array of inputs, and the second argument is the returned result.
    /// </summary>
    public Func<object[], object, bool>? Verifier { get; set; } = null;
    /// <summary>
    /// The input size to start with.
    /// </summary>
    public int InputMin { get; set; } = 1;
    /// <summary>
    /// The input size to end with.
    /// </summary>
    public int InputMax { get; set; } = 100;
    /// <summary>
    /// Number of datapoints to use between the min and maximum inputs.
    /// </summary>
    public int DataPoints { get; set; } = 20;

}
