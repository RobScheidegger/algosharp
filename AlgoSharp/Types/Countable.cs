using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Types;

public class Countable<T> : IComparable<T> where T : IComparable<T>
{
    private readonly AlgoAnalyzer analyzer;
    public Countable(T value, AlgoAnalyzer analyzer)
    {
        Value = value;
        this.analyzer = analyzer;
    }
    public T Value { get; set; }
    public static Countable<T> operator +(Countable<T> s1, Countable<T> s2)
    {
        if (s1.analyzer != s2.analyzer) throw new ArgumentException("AlgoStruct instances can only be added if they have the same analyzer.");
        dynamic value1 = s1.Value;
        dynamic value2 = s2.Value;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Additions");

        return new Countable<T>(value1 + value2, analyzer);
    }
    public static Countable<T> operator +(Countable<T> s1, T s2)
    {
        dynamic value1 = s1.Value;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Additions");

        return new Countable<T>(value1 + s2, analyzer);
    }
    public static Countable<T> operator -(Countable<T> s1, Countable<T> s2)
    {
        if (s1.analyzer != s2.analyzer) throw new ArgumentException("AlgoStruct instances can only be added if they have the same analyzer.");
        dynamic value1 = s1.Value;
        dynamic value2 = s2.Value;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Additions");

        return new Countable<T>(value1 - value2, analyzer);
    }
    public static Countable<T> operator -(Countable<T> s1, T s2)
    {
        dynamic value1 = s1.Value;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Additions");

        return new Countable<T>(value1 - s2, analyzer);
    }
    public static Countable<T> operator /(Countable<T> s1, T s2)
    {
        dynamic value1 = s1.Value;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Additions");

        return new Countable<T>(value1 / s2, analyzer);
    }
    public static bool operator <(Countable<T> s1, Countable<T> s2)
    {
        if (s1.analyzer != s2.analyzer) 
            throw new ArgumentException("AlgoStruct instances can only be added if they have the same analyzer.");
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2.Value) < 0;
    }
    public static bool operator >(Countable<T> s1, Countable<T> s2)
    {
        if (s1.analyzer != s2.analyzer) 
            throw new ArgumentException("AlgoStruct instances can only be added if they have the same analyzer.");
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2.Value) > 0;
    }
    public static bool operator >(Countable<T> s1, T s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2) > 0;
    }
    public static bool operator <(Countable<T> s1, T s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2) < 0;
    }
    public static bool operator ==(Countable<T> s1, T s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2) == 0;
    }
    public static bool operator !=(Countable<T> s1, T s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2) != 0;
    }
    public static bool operator <=(Countable<T> s1, Countable<T> s2)
    {
        return s1 == s2 || s1 < s2;
    }
    public static bool operator >=(Countable<T> s1, Countable<T> s2)
    {
        return s1 == s2 || s1 > s2;
    }
    /// <summary>
    /// Determines whether two AlgoStructs are equal by comparing their inner values.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static bool operator ==(Countable<T> s1, Countable<T> s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2.Value) == 0;
    }
    public static bool operator !=(Countable<T> s1, Countable<T> s2)
    {
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Comparisons");

        return s1.Value?.CompareTo(s2.Value) != 0;
    }
    public static Countable<T> operator %(Countable<T> s1, Countable<T> s2)
    {
        if (s1.analyzer != s2.analyzer) throw new ArgumentException("AlgoStruct instances can only be added if they have the same analyzer.");
        dynamic value1 = s1.Value;
        dynamic value2 = s2.Value;
        var value = value1 % value2;
        var analyzer = s1.analyzer;
        analyzer.Count(1, "Modulo");

        return new Countable<T>(value, analyzer);
    }
    public static Countable<T> operator ++(Countable<T> s)
    {
        dynamic startingValue = s.Value;
        s.analyzer.Count(1, "Addition");
        dynamic result = startingValue + 1;
        s.Value = result;
        return s;
    }
    public int CompareTo(T? other)
    {
        return this.Value.CompareTo(other);
    }

    public override string ToString() => Value.ToString();
}
