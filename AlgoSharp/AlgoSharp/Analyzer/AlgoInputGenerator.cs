using AlgoSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Analyzer;

public class AlgoInputGenerator
{
    private readonly Dictionary<Type, Func<int, object>> generators;
    private readonly AlgoAnalyzer analyzer;
    private readonly Random random;

    public AlgoInputGenerator(AlgoAnalyzer analyzer)
    {
        generators = new()
        {
            {typeof(AlgoArray<int>), ArrayIntGenerator}
        };
        this.analyzer = analyzer;
        random = new Random();
    }
    AlgoArray<int> ArrayIntGenerator(int n)
    {
        return new AlgoArray<int>(
            Enumerable.Range(0, n).Select(i => random.Next(int.MaxValue)),
            analyzer);
    }
    public Func<int, object> GetGenerator(Type inputType)
    {
        if(generators.ContainsKey(inputType))
        {
            return generators[inputType];
        }
        else throw new KeyNotFoundException($"The type '{inputType.FullName}' is not registered to have a " +
            $"default input generator. Either specify one manually using the InputConfiguration attribute " +
            $"or change types.");
    }
}
