using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSharp.Types
{
    public class AlgoArray<T> : IList<T> where T : IEquatable<T>, IComparable<T>
    {
        private readonly AlgoAnalyzer analyzer;
        private T[] array;
        public AlgoArray(int size, AlgoAnalyzer analyzer)
        {
            this.analyzer = analyzer;
            array = new T[size];
        }
        public AlgoArray(IEnumerable<T> array, AlgoAnalyzer analyzer)
        {
            this.analyzer = analyzer;
            this.array = array.ToArray();
        }
        public AlgoStruct<T> this[AlgoStruct<int> index] 
        {   
            get 
            {
                analyzer.Count(1, "Array Get");
                return new AlgoStruct<T>(array[index.Value], analyzer);
            } 
            set  
            {
                analyzer.Count(1, "Array Set");
                array[index.Value] = value.Value;
            }
        }

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => array.Length;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            throw new InvalidOperationException("Cannot add to a fixed size array.");
        }

        public void Add(AlgoStruct<T> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            for(int i = 0; i < array.Length; i++)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                array[i] = default;
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        public bool Contains(T item)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i]?.Equals(item) ?? (item is null))
                    return true;
            }
            return false;
        }

        public bool Contains(AlgoStruct<T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(AlgoStruct<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(AlgoStruct<T> item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, AlgoStruct<T> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new InvalidOperationException("Cannot remove elements from an array.");
        }

        public bool Remove(AlgoStruct<T> item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
