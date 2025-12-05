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
                if (set.Contains(element))
                {
                    return true;
                }
            }

            return false;
        }

        public static void ProcessElements<T, G>(this IEnumerable<T> elements, Action<G> action) where T : Component where G : Component
        {
            foreach (T target in elements)
            {
                if (target.TryGetComponent(out G damageable))
                {
                    action?.Invoke(damageable);
                }
            }
        }

        public static List<T> GetFilteredElementsInRadius<T>(Vector3 origin, float radius, LayerMask layerMask) where T : Component
        {
            Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(origin, radius, layerMask);
            List<T> filteredObjects = new List<T>();

            foreach (var collider in targetsInRadius)
            {
                if (collider.TryGetComponent(out T component))
                {
                    filteredObjects.Add(component);
                }
            }
            return filteredObjects;
        }

        public static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> source)
        {
            System.Random random = new System.Random();
            T[] array = source as T[] ?? source.ToArray();
            int n = array.Length;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array;
        }

        /// <summary>
        /// Retorna um elemento aleatório de uma coleção.
        /// </summary>
        /// <typeparam name="T">O tipo genérico dos elementos na coleção.</typeparam>
        /// <param name="source">A coleção de elementos.</param>
        /// <returns>Um elemento aleatório da coleção ou o valor padrão se a coleção estiver vazia.</returns>
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source.Any() ? source.ElementAt(Random.Range(0, source.Count())) : default;
        }

        /// <summary>
        /// Retorna um elemento aleatório do array, ignorando os elementos desejados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos</param>
        /// <param name="dataValues">Coleção de valores a serem excluídos</param>
        /// <returns>elemento aleatório do array</returns>
        public static T GetRandomExcept<T>(this IEnumerable<T> sourceArray, IEnumerable<T> dataValues)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandom();
        }

        /// <summary>
        /// Retorna um elemento aleatório do array, ignorando os elementos desejados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos</param>
        /// <param name="dataValues">Coleção de valores a serem excluídos</param>
        /// <returns>elemento aleatório do array</returns>
        public static T GetRandomExcept<T>(this IEnumerable<T> sourceArray, params T[] dataValues)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandom();
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de uma sequência limitado pelo seu tamanho.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceSequence">Sequência de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <returns>Sequência de elementos aleatórios</returns>
        public static IEnumerable<T> GetRandomAmount<T>(this IEnumerable<T> sourceSequence, int amount)
        {
            System.Random rnd = new System.Random();
            int realAmount;
            int count = sourceSequence.Count();
            if (count < amount)
            {
                realAmount = count;
            }
            else
            {
                realAmount = amount;
            }

            return (count > 0) ? sourceSequence.OrderBy(x => rnd.Next()).Take(realAmount).ToList() : default;
        }

        /// <summary>
        /// Verifica se uma sequência contém todos os elementos de outra sequência.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="a">Sequência em que serão procurados os elementos</param>
        /// <param name="b">Sequência cujos elementos serão procurados na primeira sequência</param>
        /// <returns>Retorna verdadeiro se todos os elementos da segunda sequência forem encontrados na primeira, falso caso contrário</returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }

        /// <summary>
        /// Destroi todos os componentes de uma sequencia.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="a">Sequência de componentes</param>
        public static void DestroyAllComponents<T>(this IEnumerable<T> sequence) where T : Component
        {
            foreach (T component in sequence)
            {
                Object.Destroy(component);
            }
        }

        /// <summary>
        /// Destroi todos os game objects de componentes de uma sequencia.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="a">Sequência de componentes</param>
        public static void DestroyAllGameObjects<T>(this IEnumerable<T> sequence) where T : Component
        {
            foreach (T component in sequence)
            {
                Object.Destroy(component.gameObject);
            }
        }

        /// <summary>
        /// Destroi todos os game objects de uma sequencia.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="a">Sequência de componentes</param>
        public static void DestroyAllGameObjects(this IEnumerable<GameObject> sequence)
        {
            foreach (GameObject gameObject in sequence)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}