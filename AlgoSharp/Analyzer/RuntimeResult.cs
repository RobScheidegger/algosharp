using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

public class RuntimeResult
{
    public Dictionary<string, int> Details { get; set; }
    public int[] ParameterValues { get; set; }
}
