using AlgoSharp.Analyzer;
using AlgoSharp.Types;
using System;

namespace AlgoSharp;

public class AlgoAnalyzer
{
    private readonly Dictionary<string,int> counts = new Dictionary<string,int>();
    public Delegate Algorithm { get; }
    private Func<int, object>[] CustomInputGenerators { get; set; }
    public Func<int, object>[] InputGenerators { get; }
    public AlgoInputGenerator generators;
    public AlgoAnalyzer(Delegate algorithm, params Func<int, object>[] inputGenerators)
    {
        Algorithm = algorithm;
        InputGenerators = inputGenerators;
        generators = new AlgoInputGenerator(this);
    }
    public void Count(int operations, string descriptor)
    {
        if(counts.ContainsKey(descriptor))
            counts[descriptor]++;
        else
            counts[descriptor] = operations;
    }

    public AlgoArray<T> GetArray<T>(int size) where T : IEquatable<T>, IComparable<T> => new(size, this);
    public AlgoArray<T> GetArray<T>(IEnumerable<T> arr) where T : IEquatable<T>, IComparable<T> => new(arr, this);

    public AlgoStruct<T> Get<T>(T value = default) where T : IEquatable<T>, IComparable<T> => new(value, this);

    public AlgoAnalysis Analyze()
    {
        var parameters = Algorithm.Method.GetParameters();
        bool hasCustomConfiguration = InputGenerators is not null;
        if (hasCustomConfiguration)
        {
            // Custom configuration validity checks.
            if (InputGenerators.Count() != parameters.Count())
                throw new DataMisalignedException("The specified input generators and algorithm have different input sizes.");
        }
        var parameterGenerators = new Func<int, object>[parameters.Count()];
        for(int i = 0; i < parameters.Count(); i++)
        {
            if(hasCustomConfiguration && InputGenerators[i] is not null)
            {
                parameterGenerators[i] = InputGenerators[i];
            }
            else
            {
                parameterGenerators[i] = generators.GetGenerator(parameters[i].ParameterType);
            }
        }
        
    }

    public void ResetCounts()
    {
        counts.Clear();
    }

    public void PrintResults()
    {
        foreach(var key in counts.Keys)
        {
            Console.WriteLine("{0,10}{1,10}", key, counts[key]);
        }
    }
    public int Count() => counts.Values.Sum();
}
