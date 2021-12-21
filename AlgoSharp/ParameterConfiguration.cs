using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp
{
    public class ParameterConfiguration
    {
        /// <summary>
        /// The symbolic variable name of the 
        /// </summary>
        public string VariableName { get; set; }
        /// <summary>
        /// Given the value of the symbolic variable, generates the corresponding input.
        /// </summary>
        public Func<int, object> InputGenerator { get; set; }
    }
}
