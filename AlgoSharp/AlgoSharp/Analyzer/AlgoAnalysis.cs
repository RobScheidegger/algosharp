using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp;

public class AlgoAnalysis
{
    public int ParameterCount { get; set; }
    public string Runtime { get; set; }
    public Delegate RuntimeExpression { get; set; }
    public int ComputeOperations(params int[] parameters)
    {
        return (int)RuntimeExpression.DynamicInvoke(parameters);
    }
    public bool IsLinear()
    {
        if (ParameterCount != 1) throw new NotImplementedException("IsLinear only defined for single variable functions.");
        return Runtime == "O(n)";
    }
}
