using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Gamegaard.Utils
{
    public static class ArrayUtils
    {
        public static T[] GetComponents<T>(this GameObject[] sourceList) where T : Component
        {
            List<T> components = new List<T>(sourceList.Length);
            foreach (GameObject gameObject in sourceList)
            {
                if (gameObject.TryGetComponent(out T component))
                    components.Add(component);
            }
            return components.ToArray();
        }

        public static G[] GetComponents<T, G>(this T[] sourceList) where T : Component where G : Component
        {
            List<G> components = new List<G>(sourceList.Length);
            foreach (T item in sourceList)
            {
                if (item.TryGetComponent(out G component))
                    components.Add(component);
            }
            return components.ToArray();
        }

        public static T[] GetRandomAmount<T>(this T[] sourceArray, int amount, bool allowDuplicates = false)
        {
            if (sourceArray == null || sourceArray.Length == 0 || amount <= 0)
                return Array.Empty<T>();

            Random random = new Random();

            if (allowDuplicates)
            {
                T[] result = new T[amount];
                for (int i = 0; i < amount; i++)
                    result[i] = sourceArray[random.Next(sourceArray.Length)];
                return result;
            }

            int clampedAmount = Math.Min(amount, sourceArray.Length);
            T[] pool = (T[])sourceArray.Clone();

            for (int i = pool.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (pool[i], pool[j]) = (pool[j], pool[i]);
            }

            T[] selected = new T[clampedAmount];
            Array.Copy(pool, selected, clampedAmount);
            return selected;
        }

        public static T[] GetRandomAmount<T>(this T[] sourceArray, int numberOfItems, Func<T, bool> criteria, bool allowDuplicates = false)
        {
            if (sourceArray == null || numberOfItems <= 0)
                return Array.Empty<T>();

            T[] matchingItems = sourceArray.Where(criteria).ToArray();
            return matchingItems.GetRandomAmount(numberOfItems, allowDuplicates);
        }

        public static T[] GetRandomIntersect<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            T[] intersectedValues = sourceArray.Intersect(dataValues).ToArray();
            return intersectedValues.GetRandomAmount(amount, allowDuplicates);
        }

        public static T[] GetRandomExcept<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandomAmount(amount, allowDuplicates);
        }

        public static int LengthLessOne<T>(this T[] sourceArray)
        {
            return Math.Max(sourceArray.Length - 1, 0);
        }

        public static bool FindIndex<T>(this T[] sourceArray, T item, out int value)
        {
            value = Array.FindIndex(sourceArray, val => val.Equals(item));
            return value != -1;
        }

        public static int FindIndex<T>(this T[] sourceArray, T item)
        {
            return Array.FindIndex(sourceArray, val => val.Equals(item));
        }

        public static T[] ShuffledOrder<T>(this T[] sourceArray)
        {
            Random random = new Random();
            T[] copy = (T[])sourceArray.Clone();

            for (int i = copy.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (copy[i], copy[j]) = (copy[j], copy[i]);
            }

            return copy;
        }
    }
}