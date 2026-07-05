using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class ListUtils
    {
        private static readonly System.Random random = new System.Random();

        public static List<T> GetComponents<T>(this IReadOnlyList<GameObject> sourceList) where T : Component
        {
            List<T> components = new List<T>(sourceList.Count);
            foreach (GameObject gameObject in sourceList)
            {
                if (gameObject.TryGetComponent(out T component))
                    components.Add(component);
            }
            return components;
        }

        public static List<G> GetComponents<T, G>(this IReadOnlyList<T> sourceList) where T : Component where G : Component
        {
            List<G> components = new List<G>(sourceList.Count);
            foreach (T item in sourceList)
            {
                if (item.TryGetComponent(out G component))
                    components.Add(component);
            }
            return components;
        }

        public static List<T> GetRandomAmount<T>(this IReadOnlyList<T> sourceList, int amount, bool allowDuplicates = false)
        {
            if (sourceList == null || sourceList.Count == 0 || amount <= 0)
                return new List<T>();

            if (allowDuplicates)
            {
                List<T> result = new List<T>(amount);
                for (int i = 0; i < amount; i++)
                    result.Add(sourceList[random.Next(sourceList.Count)]);
                return result;
            }

            int clampedAmount = Math.Min(amount, sourceList.Count);

            if (clampedAmount == sourceList.Count)
                return new List<T>(sourceList);

            List<T> pool = new List<T>(sourceList);
            FisherYatesShuffle(pool);
            return pool.GetRange(0, clampedAmount);
        }

        public static List<T> GetRandomAmount<T>(this IReadOnlyList<T> sourceList, int amount, Func<T, bool> criteria, bool allowDuplicates = false)
        {
            if (sourceList == null || amount <= 0)
                return new List<T>();

            List<T> matchingItems = new List<T>();
            foreach (T item in sourceList)
            {
                if (criteria(item))
                    matchingItems.Add(item);
            }

            return matchingItems.GetRandomAmount(amount, allowDuplicates);
        }

        public static List<T> GetRandomIntersect<T>(this IReadOnlyList<T> sourceList, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            HashSet<T> valueSet = new HashSet<T>(dataValues);
            List<T> intersected = new List<T>();
            foreach (T item in sourceList)
            {
                if (valueSet.Contains(item))
                    intersected.Add(item);
            }
            return intersected.GetRandomAmount(amount, allowDuplicates);
        }

        public static List<T> GetRandomExcept<T>(this IReadOnlyList<T> sourceList, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            HashSet<T> valueSet = new HashSet<T>(dataValues);
            List<T> excepted = new List<T>();
            foreach (T item in sourceList)
            {
                if (!valueSet.Contains(item))
                    excepted.Add(item);
            }
            return excepted.GetRandomAmount(amount, allowDuplicates);
        }

        public static int FinalIndex<T>(this IReadOnlyList<T> sourceList)
        {
            return Math.Max(sourceList.Count - 1, 0);
        }

        public static bool IsFull<T>(this List<T> sourceList)
        {
            return sourceList.Count == sourceList.Capacity;
        }

        public static int RemainingSpace<T>(this List<T> sourceList)
        {
            return sourceList.Capacity - sourceList.Count;
        }

        public static List<T> ShuffledOrder<T>(this IReadOnlyList<T> sourceList)
        {
            List<T> copy = new List<T>(sourceList);
            FisherYatesShuffle(copy);
            return copy;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            FisherYatesShuffle(list);
        }

        public static List<T> Combine<T>(this List<T> sourceList, List<T> otherList)
        {
            List<T> combined = new List<T>(sourceList.Count + otherList.Count);
            combined.AddRange(sourceList);
            combined.AddRange(otherList);
            return combined;
        }

        private static void FisherYatesShuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}