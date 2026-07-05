using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Gamegaard.Utils
{
    public static class IEnumerableUtils
    {
        public static void HandleCollisionsInRadius2D<T>(Vector3 origin, float radius, LayerMask layerMask, Action<T> action) where T : Component
        {
            Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(origin, radius, layerMask);
            targetsInRadius.ProcessElements(action);
        }

        public static bool HasIntersection<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null || second == null) return false;

            HashSet<T> set = new HashSet<T>(first);
            foreach (T element in second)
            {
                if (set.Contains(element)) return true;
            }
            return false;
        }

        public static void ProcessElements<T, G>(this IEnumerable<T> elements, Action<G> action) where T : Component where G : Component
        {
            T[] materialized = elements as T[] ?? elements.ToArray();
            foreach (T target in materialized)
            {
                if (target.TryGetComponent(out G component))
                    action?.Invoke(component);
            }
        }

        public static List<T> GetFilteredElementsInRadius<T>(Vector3 origin, float radius, LayerMask layerMask) where T : Component
        {
            Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(origin, radius, layerMask);
            List<T> filteredObjects = new List<T>(targetsInRadius.Length);

            foreach (Collider2D collider in targetsInRadius)
            {
                if (collider.TryGetComponent(out T component))
                    filteredObjects.Add(component);
            }
            return filteredObjects;
        }

        public static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> source)
        {
            System.Random random = new System.Random();
            T[] array = source as T[] ?? source.ToArray();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array;
        }

        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            T[] array = source as T[] ?? source.ToArray();
            return array.Length > 0 ? array[Random.Range(0, array.Length)] : default;
        }

        public static T GetRandomExcept<T>(this IEnumerable<T> sourceArray, IEnumerable<T> dataValues)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandom();
        }

        public static T GetRandomExcept<T>(this IEnumerable<T> sourceArray, params T[] dataValues)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandom();
        }

        public static IEnumerable<T> GetRandomAmount<T>(this IEnumerable<T> sourceSequence, int amount)
        {
            if (amount <= 0) return Enumerable.Empty<T>();

            T[] array = sourceSequence as T[] ?? sourceSequence.ToArray();
            if (array.Length == 0) return Enumerable.Empty<T>();

            int clampedAmount = Math.Min(amount, array.Length);
            System.Random random = new System.Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array.Take(clampedAmount).ToList();
        }

        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }

        public static void DestroyAllComponents<T>(this IEnumerable<T> sequence) where T : Component
        {
            foreach (T component in sequence)
                Object.Destroy(component);
        }

        public static void DestroyAllGameObjects<T>(this IEnumerable<T> sequence) where T : Component
        {
            foreach (T component in sequence)
                Object.Destroy(component.gameObject);
        }

        public static void DestroyAllGameObjects(this IEnumerable<GameObject> sequence)
        {
            foreach (GameObject gameObject in sequence)
                Object.Destroy(gameObject);
        }
    }
}