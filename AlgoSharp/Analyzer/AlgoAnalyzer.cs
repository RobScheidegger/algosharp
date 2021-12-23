using AlgoSharp.Analyzer;
using AlgoSharp.Types;
using System;

namespace AlgoSharp;

public class AlgoAnalyzer
{
    private Dictionary<string,int> Counts = new Dictionary<string,int>();
    private Func<int, object>[] CustomInputGenerators { get; set; }
    private AnalyzerConfiguration Configuration { get; set; }
    private RuntimeRegressionAnalyzer RuntimeAnalyzer { get; set; }
    public AlgoInputGenerator generators;
    public Delegate Algorithm { get; private set; }
    public Func<int, object>[] InputGenerators { get; private set; }
    public int ParameterCount { get; private set; }
    public AlgoAnalyzer()
    {
        generators = new AlgoInputGenerator(this);
        Configuration = new AnalyzerConfiguration();
        RuntimeAnalyzer = new();
    }
    public void Count(int operations, string descriptor)
    {
        if(Counts.ContainsKey(descriptor))
            Counts[descriptor]++;
        else
            Counts[descriptor] = operations;
    }

    public AlgoArray<T> GetArray<T>(int size) where T : IEquatable<T>, IComparable<T> => new(size, this);
    public AlgoArray<T> GetArray<T>(IEnumerable<T> arr) where T : IEquatable<T>, IComparable<T> => new(arr, this);
    public AlgoStruct<T> Get<T>(T value = default) where T : IEquatable<T>, IComparable<T> => new(value, this);
    public void Configure(Action<AnalyzerConfiguration> action) => action(Configuration);
    public AlgoAnalysis Analyze(Delegate algorithm, params Func<int, object>[] inputGenerators)
    {
        Algorithm = algorithm;
        ParameterCount = algorithm?.Method.GetParameters().Length ?? 0;
        InputGenerators = inputGenerators;
        if (Algorithm is null)
            throw new Exception("Algorithm not specified.");

        var generators = GetParameterGenerators();

        IEnumerable<RuntimeResult> results = PerformAnalysisTrials(generators);

        return GenerateAnalysis(results);
    }

    private AlgoAnalysis GenerateAnalysis(IEnumerable<RuntimeResult> results)
    {
        // General runtime result
        var generalRuntime = RuntimeAnalyzer.FitRuntime(results, ParameterCount);

        var distinctCategories = results.SelectMany(i => i.Details.Keys.AsEnumerable()).Distinct();

        var categoryRuntimes = distinctCategories.Select(i => RuntimeAnalyzer.FitRuntime(results, ParameterCount, i));

        var categoryAnalysis = categoryRuntimes.Select(i => new AlgoAnalysis()
        {
            Coefficient = i.Coefficient,
            Confidence = i.RSquared,
            Name = i.Category,
            Runtime = i.Name
        });
        
        return new AlgoAnalysis()
        {
            Runtime = $"O({generalRuntime.Name})",
            Name = "Overall Runtime",
            Coefficient = generalRuntime.Coefficient,
            Confidence = generalRuntime.RSquared,
            SubCategories = categoryAnalysis
        };
    }

    private IEnumerable<RuntimeResult> PerformAnalysisTrials(Func<int, object>[] generators)
    {
        ResetCounts();
        List<RuntimeResult> results = new List<RuntimeResult>();
        foreach(var input in EnumerateInputSizes(ParameterCount))
        {
            var inputs = input.Zip(generators).Select((input) => input.Second(input.First));
            
            var result = Algorithm.DynamicInvoke(inputs.ToArray());
            if(Configuration.Verifier is not null)
            {
                var valid = Configuration.Verifier(inputs.ToArray(), result);
                if (!valid)
                    throw new Exception("Found invalid result with inputs {inputs} and result {result}");
            }
            var runtime = new RuntimeResult()
            {
                Details = new(Counts),
                ParameterValues = input.ToArray()
            };
            results.Add(runtime);
            ResetCounts();
        }
        return results;
    }

    private IEnumerable<List<int>> EnumerateInputSizes(int remainingSize)
    {
        if(remainingSize == 0)
        {
            return new List<List<int>>();
        }
        else
        {
            int increment = (Configuration.InputMax - Configuration.InputMin) / Configuration.DataPoints;
            int start = Configuration.InputMin;
            var startList = Enumerable.Range(0, Configuration.DataPoints)
                .Select(i => start + i * increment)
                .Select(i => new List<int>() { i })
                .ToList();

            if (remainingSize == 1)
                return startList;
            else
            {
                var subLists = EnumerateInputSizes(remainingSize - 1);
                return subLists.Join(startList, a => true, b => true, (a, b) => a.Append(b[0]).ToList());
            }
        }
    }

    private Func<int, object>[] GetParameterGenerators()
    {
        var parameters = Algorithm.Method.GetParameters();
        bool hasCustomConfiguration = InputGenerators is not null && InputGenerators.Count() > 0;
        if (hasCustomConfiguration)
        {
            // Custom configuration validity checks.
            if (InputGenerators.Count() != parameters.Count())
                throw new DataMisalignedException("The specified input generators and algorithm have different input sizes.");
        }
        var parameterGenerators = new Func<int, object>[parameters.Count()];
        for (int i = 0; i < parameters.Count(); i++)
        {
            if (hasCustomConfiguration && InputGenerators[i] is not null)
            {
                parameterGenerators[i] = InputGenerators[i];
            }
            else
            {
                parameterGenerators[i] = generators.GetGenerator(parameters[i].ParameterType);
            }
        }
        return parameterGenerators;
    }

    public void ResetCounts()
    {
        Counts = new();
    }

    public void PrintResults()
    {
        foreach(var key in Counts.Keys)
        {
            Console.WriteLine("{0,10}{1,10}", key, Counts[key]);
        }
    }
    public void UseAlgorithm(Delegate algorithm) => Algorithm = algorithm;
    public int Count() => Counts.Values.Sum();
}
