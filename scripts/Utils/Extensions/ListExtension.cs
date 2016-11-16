using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.Utils.Extensions
{
    /// <summary>
    /// Extensions for <see cref="List{T}"/> class.
    /// </summary>
    public static class ListExtension
    {
        public static void AddOnce<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static T BinaryFind<T>(this IList<T> list, Func<T, int> comparer)
        {
            int num = list.BinarySearch(comparer);
            if (num < 0)
            {
                return default(T);
            }
            return list[num];
        }

        public static int BinarySearch<T>(this IList<T> list, Func<T, int> comparer)
        {
            int i = 0;
            int num = list.Count - 1;
            while (i <= num)
            {
                int num2 = i + num >> 1;
                int num3 = comparer(list[num2]);
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

        public static int BinarySearch<T, U>(this IList<T> list, U compared, Func<T, U, int> comparer)
        {
            int i = 0;
            int num = list.Count - 1;
            while (i <= num)
            {
                int num2 = i + num >> 1;
                int num3 = comparer(list[num2], compared);
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

        public static List<T> Randomize<T>(this IList<T> list, System.Random random = null)
        {
            List<T> list2 = new List<T>();
            if (random == null)
            {
                random = new System.Random();
            }
            while (list.Count > 0)
            {
                int index = random.Next(0, list.Count);
                list2.Add(list[index]);
                list.RemoveAt(index);
            }
            return list2;
        }

        public static void Shuffle<T>(this IList<T> list, System.Random random = null)
        {
            if (list == null)
            {
                return;
            }
            if (random == null)
            {
                random = new System.Random();
            }
            for (int i = 0; i < list.Count - 2; i++)
            {
                int index = random.Next(i, list.Count);
                T value = list[index];
                list[index] = list[i];
                list[i] = value;
            }
        }

        public static string ToString<T>(this IEnumerable<T> list, Func<T, string> stringifier, string separator = ",", string defaultIfEmpty = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (T current in list)
            {
                string text = stringifier(current);
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(separator);
                }
                stringBuilder.Append((!string.IsNullOrEmpty(text)) ? text : defaultIfEmpty);
            }
            return stringBuilder.ToString();
        }

        public static string ToString<T>(this IEnumerable<T> list, string separator = ",", string defaultIfEmpty = "")
        {
            return list.ToString((T t) => t.ToString(), separator, defaultIfEmpty);
        }

        public static T FindHighest<T>(this IEnumerable<T> list, Func<T, float> scorer, Func<T, bool> predicate = null)
        {
            T result = default(T);
            float num = -3.40282347E+38f;
            foreach (T current in list)
            {
                if (predicate == null || predicate(current))
                {
                    float num2 = scorer(current);
                    if (num2 > num)
                    {
                        num = num2;
                        result = current;
                    }
                }
            }
            return result;
        }

        public static T FindLowest<T>(this IList<T> list, Func<T, float> scorer, Func<T, bool> predicate = null)
        {
            return list.FindHighest((T element) => -1f * scorer(element), predicate);
        }

        public static void Shuffle<T>(this List<T> list, Random random)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int index = random.Next(0, i);
                T value = list[i];
                list[i] = list[index];
                list[index] = value;
            }
        }

        public static void MoveItemAtIndexToFront<T>(this List<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            list.Insert(0, item);
        }
    }
}
