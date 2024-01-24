using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Gamegaard.Utils
{
    public static class ArrayUtils
    {      
        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de um array.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <param name="allowDuplicates">Indica se permite valores duplicados</param>
        /// <returns>Array de elementos aleatórios</returns>
        public static T[] GetRandomAmount<T>(this T[] sourceArray, int amount, bool allowDuplicates = false)
        {
            if (sourceArray == null || amount <= 0)
            {
                return Array.Empty<T>();
            }

            Random rnd = new Random();
            List<T> shuffledList = sourceArray.OrderBy(x => rnd.Next()).ToList();

            if (!allowDuplicates && shuffledList.Count <= amount)
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

            return selectedItems.ToArray();
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de um array com base em critérios.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos de origem</param>
        /// <param name="numberOfItems">Número de itens a serem obtidos</param>
        /// <param name="criteria">Critério de seleção</param>
        /// <param name="allowDuplicates">Indica se permite valores duplicados</param>
        /// <returns>Array de elementos aleatórios com base nos critérios</returns>
        public static T[] GetRandomAmount<T>(this T[] sourceArray, int numberOfItems, Func<T, bool> criteria, bool allowDuplicates = false)
        {
            List<T> selectedItems = new List<T>();
            List<T> matchingItems = sourceArray.Where(item => criteria(item)).ToList();

            int remainingItemsCount = numberOfItems;

            if (allowDuplicates)
            {
                selectedItems.AddRange(matchingItems);
                remainingItemsCount -= matchingItems.Count;
            }

            List<T> remainingItems = matchingItems.ToList();
            Random random = new Random();

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

            return selectedItems.ToArray();
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos da interseção entre um array e uma coleção de valores.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos de origem</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos na interseção</param>
        /// <param name="dataValues">Coleção de valores para a interseção</param>
        /// <param name="allowDuplicates">Indica se permite valores duplicados</param>
        /// <returns>Array de elementos aleatórios na interseção</returns>
        public static T[] GetRandomIntersect<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            T[] intersectedValues = sourceArray.Intersect(dataValues).ToArray();
            return intersectedValues.GetRandomAmount(amount);
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos que não estão em uma coleção de valores.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos de origem</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos excluindo valores da coleção</param>
        /// <param name="dataValues">Coleção de valores a serem excluídos</param>
        /// <param name="allowDuplicates">Indica se permite valores duplicados</param>
        /// <returns>Array de elementos aleatórios excluindo valores da coleção</returns>
        public static T[] GetRandomExcept<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues, bool allowDuplicates = false)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandomAmount(amount);
        }

        /// <summary>
        /// Calcula o comprimento de um array menos 1, garantindo que o resultado não seja menor que 0.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array cujo comprimento será calculado</param>
        /// <returns>Comprimento do array menos 1, não negativo</returns>
        public static int LengthLessOne<T>(this T[] sourceArray)
        {
            return (int)Mathf.Clamp(sourceArray.Length - 1, 0, float.MaxValue);
        }

        /// <summary>
        /// Encontra o índice de um item em um array genérico.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array genérico em que o item será procurado</param>
        /// <param name="item">Item a ser procurado no array</param>
        /// <param name="value">Índice do item encontrado</param>
        /// <returns>Retorna verdadeiro se o item for encontrado no array, falso caso contrário</returns>
        public static bool FindIndex<T>(this T[] sourceArray, T item, out int value)
        {
            value = Array.FindIndex(sourceArray, val => val.Equals(item));
            return value != -1;
        }

        /// <summary>
        /// Esta extensão retorna o índice do primeiro elemento na array que é igual ao item especificado.
        /// </summary>
        /// <typeparam name="T">O tipo de elementos da array</typeparam>
        /// <param name="sourceArray">A array na qual será feita a busca</param>
        /// <param name="item">O item que será procurado</param>
        /// <returns>O índice do primeiro elemento igual ao item especificado, ou -1 se o item não for encontrado.</returns>
        public static int FindIndex<T>(this T[] sourceArray, T item)
        {
            return Array.FindIndex(sourceArray, val => val.Equals(item));
        }

        /// <summary>
        /// Ordena uma lista de forma aleatória.
        /// </summary>
        /// <typeparam name="T">Tipo genérico da lista.</typeparam>
        /// <param name="sourceArray">Array a ser embaralhado.</param>
        /// <returns>Retorna uma nova lista com os elementos embaralhados.</returns>
        public static T[] ShuffledOrder<T>(this T[] sourceArray)
        {
            Random random = new Random();
            return sourceArray.OrderBy(x => random.Next()).ToArray();
        }
    }
}