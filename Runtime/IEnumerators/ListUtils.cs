using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class ListUtils
    {
        public static List<T> GetComponents<T>(this List<GameObject> sourceList) where T : Component
        {
            List<T> components = new List<T>();

            foreach (GameObject t in sourceList)
            {
                if (t.TryGetComponent(out T component))
                {
                    components.Add(component);
                }
            }

            return components;
        }

        public static List<G> GetComponents<T, G>(this List<T> sourceList) where T : Component where G : Component
        {
            List<G> components = new List<G>();

            foreach (T t in sourceList)
            {
                if (t.TryGetComponent(out G component))
                {
                    components.Add(component);
                }
            }

            return components;
        }

        #region List
        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceList">Lista de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <returns>Retorna uma lista de elementos aleatórios</returns>
        public static List<T> GetRandomAmount<T>(this List<T> sourceList, int amount, bool allowDuplicates = false)
        {
            if (sourceList == null || amount <= 0)
            {
                return new List<T>();
            }

            System.Random rnd = new System.Random();
            List<T> shuffledList = sourceList.OrderBy(x => rnd.Next()).ToList();

            if (!allowDuplicates && shuffledList.Count < amount)
            {
                Debug.LogWarning("GetRandom with reduced amount. The list has fewer elements than required.");
                amount = shuffledList.Count;
            }

            HashSet<T> selectedSet = new HashSet<T>();
            List<T> selectedItems = new List<T>();

            while (selectedItems.Count < amount)
            {
                int index = rnd.Next(shuffledList.Count);
                T selectedItem = shuffledList[index];

                if (allowDuplicates || selectedSet.Add(selectedItem))
                {
                    selectedItems.Add(selectedItem);
                }
            }

            return selectedItems;
        }

        public static List<T> GetRandomAmount<T>(this List<T> sourceList, int numberOfItems, Func<T, bool> criteria, bool allowDuplicates = false)
        {
            List<T> selectedItems = new List<T>();
            List<T> matchingItems = sourceList.Where(item => criteria(item)).ToList();

            int remainingItemsCount = numberOfItems;

            if (allowDuplicates)
            {
                selectedItems.AddRange(matchingItems);
                remainingItemsCount -= matchingItems.Count;
            }

            List<T> remainingItems = matchingItems.ToList();
            System.Random random = new System.Random();

            while (remainingItemsCount > 0 && remainingItems.Count > 0)
            {
                int next = random.Next(remainingItems.Count);
                selectedItems.Add(remainingItems[next]);
                remainingItemsCount--;
                if (!allowDuplicates)
                {
                    remainingItems.RemoveAt(next);
                }
            }

            return selectedItems;
        }

        public static List<T> GetRandomIntersect<T>(this List<T> sourceList, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            List<T> intersectedValues = sourceList.Intersect(dataValues).ToList();
            return intersectedValues.GetRandomAmount(amount, allowDuplicates);
        }

        public static List<T> GetRandomExcept<T>(this List<T> sourceList, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            List<T> valuesExcept = sourceList.Except(dataValues).ToList();
            return valuesExcept.GetRandomAmount(amount, allowDuplicates);
        }

        /// <summary>
        /// Obtém o valor inteiro da contagem da lista menos um, usando a função Clamp do Mathf para garantir que o resultado seja sempre entre 0 e float.MaxValue.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sourceList">A lista a ser contada.</param>
        /// <returns>Retorna o valor inteiro da contagem da lista menos um, nunca negativo</returns>
        public static int FinalIndex<T>(this List<T> sourceList)
        {
            return (int)Mathf.Clamp(sourceList.Count - 1, 0, float.MaxValue);
        }

        /// <summary>
        /// Verifica se a lista atingiu sua capacidade máxima de elementos.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sourceList">A lista a ser verificada.</param>
        /// <returns>Retorna true se a lista estiver cheia</returns>
        public static bool IsFull<T>(this List<T> sourceList)
        {
            return sourceList.Count == sourceList.Capacity;
        }

        /// <summary>
        /// Obtém o espaço restante na capacidade da lista.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sourceList">A lista a ser verificada.</param>
        /// <returns>Retorna o espaço restante na capacidade da lista.</returns>
        public static int RemainingSpace<T>(this List<T> sourceList)
        {
            return sourceList.Capacity - sourceList.Count;
        }

        /// <summary>
        /// Ordena uma lista de forma aleatória.
        /// </summary>
        /// <typeparam name="T">Tipo genérico da lista.</typeparam>
        /// <param name="sourceList">Lista a ser embaralhada.</param>
        /// <returns>Retorna uma nova lista com os elementos embaralhados.</returns>
        public static List<T> ShuffledOrder<T>(this List<T> sourceList)
        {
            System.Random random = new System.Random();
            return sourceList.OrderBy(x => random.Next()).ToList();
        }

        /// <summary>
        /// Combina duas listas em uma nova.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento das listas a serem combinadas.</typeparam>
        /// <param name="sourceList">A primeira lista a ser combinada.</param>
        /// <param name="otherList">A segunda lista a ser combinada.</param>
        /// <returns>Retorna uma nova Lista com os elementos de A e B.</returns>
        public static List<T> Combine<T>(this List<T> sourceList, List<T> otherList)
        {
            int arraySize = sourceList.Count + otherList.Count;
            T[] combined = new T[arraySize];

            sourceList.CopyTo(combined);
            otherList.CopyTo(combined, sourceList.Count);
            return new List<T>(combined);
        }
        #endregion
    }
}