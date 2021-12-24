using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Types;

public class CountableDictionary<KeyType, ValueType> 
    where KeyType : IComparable<KeyType> 
    where ValueType : IComparable<ValueType>
{
    private readonly Dictionary<KeyType, ValueType> dictionary = new();
    private readonly AlgoAnalyzer analyzer;

    public CountableDictionary(AlgoAnalyzer analyzer)
    {
        this.analyzer = analyzer;
    }
    public Countable<ValueType> this[Countable<KeyType> key]
    {
        get
        {
            analyzer.Count(1, "Dictionary Get");
            return new Countable<ValueType>(dictionary[key.Value], analyzer);
        }
        set
        {
            dictionary[key.Value] = value.Value;
            analyzer.Count(1, "Dictionary Set");
        }
    }

    public IEnumerable<Countable<KeyType>> Keys
    {
        get
        {
            analyzer.Count(dictionary.Count, "Dictionary Get");
            return dictionary.Keys.Select(i => new Countable<KeyType>(i, analyzer));
        }
    }

    public IEnumerable<Countable<ValueType>> Values
    {
        get
        {
            analyzer.Count(dictionary.Count, "Dictionary Set");
            return dictionary.Values.Select(i => new Countable<ValueType>(i, analyzer));
        }
    }

    public int Count
    {
        get
        {
            analyzer.Count(1, "Get Dictionary Count");
            return dictionary.Count;
        }
    }

    public bool IsReadOnly => false;

    public void Add(Countable<KeyType> key, Countable<ValueType> value)
    {
        analyzer.Count(1, "Dictionary Add");
        dictionary.Add(key.Value, value.Value);
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(Countable<KeyType> key, Countable<ValueType> value)
    {
        analyzer.Count(1, "Dictionary Contains");
        return dictionary.ContainsKey(key.Value) && 
            (dictionary[key.Value]?.Equals(value.Value) ?? (value.Value is null));
    }

    public bool ContainsKey(Countable<KeyType> key)
    {
        analyzer.Count(1, "Dictionary Contains");
        return dictionary.ContainsKey(key.Value);
    }

    public void Remove(Countable<KeyType> key)
    {
        analyzer.Count(2, "Dictionary Remove");
        if(dictionary.ContainsKey(key.Value))
            dictionary.Remove(key.Value);

    }
}
