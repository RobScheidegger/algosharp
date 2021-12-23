using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp;

public class AlgoAnalysis
{
    /// <summary>
    /// The name of the category that this analysis represents.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The found runtime of the algorithm (given in Big-O notation).
    /// </summary>
    public string Runtime { get; set; }
    /// <summary>
    /// A measure of how confident that we are that the algorithm does in fact have this runtime.
    /// </summary>
    public double Confidence { get; set; }
    /// <summary>
    /// The leading coefficient on the Big-O term.
    /// </summary>
    public double Coefficient { get; set; }
    /// <summary>
    /// List of sub categories of the relevant analysis.
    /// </summary>
    public IEnumerable<AlgoAnalysis> SubCategories { get; set; }
    public bool IsLinear()
    {
        return Runtime == "O(n)";
    }
    public bool IsQuadratic()
    {
        return Runtime == "O(n^2)";
    }
    public bool IsLogarithmic()
    {
        return Runtime == "O(log n)";
    }
    public override string ToString()
    {
        return $"{Name} - {Runtime}";
    }
}