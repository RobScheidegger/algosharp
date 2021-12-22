using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

/// <summary>
/// Tool to analyze the individual running times of many trials for different input sizes and 
/// determine the asymptotic runtime from that data.
/// </summary>
public class RuntimeRegressionAnalyzer
{
    private static readonly IEnumerable<RuntimeDefinition> Definitions = new[]
    {
        new RuntimeDefinition()
        {
            Name = "1",
            Transformation = n => 0
        },
        new RuntimeDefinition()
        {
            Name = "n",
            Transformation = n => n
        },
        new RuntimeDefinition()
        {
            Name = "n^2",
            Transformation = n => n * n
        }
    };

    public RuntimeDefinition FitRuntime(IEnumerable<RuntimeResult> results, int parameterCount, string detailName = null)
    {
        if (parameterCount > 1)
            throw new Exception("Fit currently only configured to run with a single parameter.");
        for(int i = 0; i < parameterCount; i++)
        {
            var points = results.Select(j =>
            {
                if (detailName is not null && !j.Details.ContainsKey(detailName))
                    return null;

                return new
                {
                    X = j.ParameterValues[i],
                    Y = detailName is not null ? j.Details[detailName] : j.Details.Sum(i => i.Value)
                };
            }).Where(j => j is not null);

            var regressionResults = Definitions.Select(definition =>
            {
                var xvalues = points.Select(j => definition.Transformation(j.X));
                var yvalues = points.Select(j => (double)j.Y);

                var (b, m) = Fit.Line(xvalues.ToArray(), yvalues.ToArray());
                var predicted = xvalues.Select(j => m * j + b);
                var rsquared = GoodnessOfFit.RSquared(yvalues, predicted);
                return new RuntimeDefinition()
                {
                    Name = definition.Name,
                    Transformation = definition.Transformation,
                    RSquared = rsquared
                };
            });
            return regressionResults.OrderByDescending(j => j.RSquared).First();
        }
        return null;
    }
}
