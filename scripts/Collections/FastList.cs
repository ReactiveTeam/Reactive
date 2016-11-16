using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reactive.Collections
{
    public class FastList<T> : IEnumerable, IEnumerable<T> where T : IEquatable<T>
    {
        private const int DefaultSize = 4;

        public T[] Data = new T[4];

        protected int count;

        protected int capacity = 4;

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                int num = this.capacity;
                this.capacity = Math.Max(value, this.Count);
                if (this.capacity != num)
                {
                    T[] array = new T[this.capacity];
                    Array.Copy(this.Data, array, this.Count);
                    this.Data = array;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public FastList(int capacity = -1)
        {
            if (capacity <= 0)
            {
                capacity = 4;
            }
            this.Capacity = capacity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        public void Add(T element)
        {
            if (this.Count == this.Capacity)
            {
                this.Capacity *= 2;
            }
            this.Data[this.count] = element;
            this.count++;
        }

        public int BinarySearch<U>(U compared, Func<T, U, int> comparer)
        {
            int i = 0;
            int num = this.count - 1;
            while (i <= num)
            {
                int num2 = i + num >> 1;
                int num3 = comparer(this.Data[num2], compared);
                if (num3 == 0)
                {
                    return num2;
                }
                if (num3 < 0)
                {
                    i = num2 + 1;
                }
                else
                {
                    num = num2 - 1;
                }
            }
            return ~i;
        }

        public void Clear()
        {
            this.count = 0;
        }

        public void Insert(int index, T element)
        {
            if (index > this.count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (this.Count == this.Capacity)
            {
                this.Capacity *= 2;
            }
            if (index < this.count)
            {
                Array.Copy(this.Data, index, this.Data, index + 1, this.Count - index);
            }
            this.Data[index] = element;
            this.count++;
        }

        public bool Remove(T element)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.Data[i].Equals(element))
                {
                    if (i == this.count - 1)
                    {
                        this.Data[i] = default(T);
                    }
                    else
                    {
                        Array.Copy(this.Data, i + 1, this.Data, i, this.Count - i - 1);
                    }
                    this.count--;
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == this.count - 1)
            {
                this.Data[index] = default(T);
            }
            else
            {
                Array.Copy(this.Data, index + 1, this.Data, index, this.Count - index - 1);
            }
            this.count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
