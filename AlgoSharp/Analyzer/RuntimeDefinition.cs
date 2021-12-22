using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

/// <summary>
/// Defines a possible runtime of an algorithm.
/// </summary>
public class RuntimeDefinition
{
    /// <summary>
    /// The name of the operation that this runtime represents. 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The transformation to apply to the x-coordinate in linear regression ot 
    /// </summary>
    public Func<int, double>  Transformation { get; set; } 
    /// <summary>
    /// The r^2 value of the movel attached to this prediction, as a measure of confidences in our accuracy.
    /// </summary>
    public double RSquared { get; set; }
    /// <summary>
    /// The runtime category that this runtime represents (e.x. 'Array Get')
    /// </summary>
    public string Category { get; set; }
    /// <summary>
    /// The estimated coefficient in front of the leading term.
    /// </summary>
    public double Coefficient { get; set; }
}
