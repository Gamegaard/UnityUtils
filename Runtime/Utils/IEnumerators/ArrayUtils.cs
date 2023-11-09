using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils.Runtime
{
    public static class ArrayUtils
    {
        /// <summary>
        /// Retorna um elemento aleatório do array.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sequence">Array de elementos</param>
        /// <returns>elemento aleatório do array</returns>
        public static T GetRandom<T>(this T[] sequence)
        {
            return sequence.Length > 0 ? sequence[Random.Range(0, sequence.Length)] : default;
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de um array.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sequence">Array de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <returns>Array de elementos aleatórios</returns>
        public static T[] GetRandomAmount<T>(this T[] sequence, int amount)
        {
            System.Random rnd = new System.Random();
            int realAmount;
            int count = sequence.Count();
            if (count < amount)
            {
                realAmount = count;
            }
            else
            {
                Debug.LogWarning("GetRandom with reduced amount. The list has less elements than required.");
                realAmount = amount;
            }

            return (count > 0) ? sequence.OrderBy(x => rnd.Next()).Take(realAmount).ToArray() : default;
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de um array com base em critérios.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceArray">Array de elementos de origem</param>
        /// <param name="numberOfItems">Número de itens a serem obtidos</param>
        /// <param name="criteria">Critério de seleção</param>
        /// <returns>Array de elementos aleatórios com base nos critérios</returns>
        public static T[] GetRandomAmount<T>(this T[] sourceArray, int numberOfItems, System.Func<T, bool> criteria)
        {
            System.Random random = new System.Random();
            List<T> selectedItems = new List<T>();
            List<T> matchingItems = sourceArray.Where(item => criteria(item)).ToList();

            selectedItems.AddRange(matchingItems);
            int remainingItemsCount = numberOfItems - matchingItems.Count;

            List<T> remainingItems = sourceArray.Where(item => !criteria(item) && !selectedItems.Contains(item)).ToList();

            while (remainingItemsCount > 0 && remainingItems.Count > 0)
            {
                int index = random.Next(remainingItems.Count);
                selectedItems.Add(remainingItems[index]);
                remainingItems.RemoveAt(index);
                remainingItemsCount--;
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
        /// <returns>Array de elementos aleatórios na interseção</returns>
        public static T[] GetRandomIntersect<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues)
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
        /// <returns>Array de elementos aleatórios excluindo valores da coleção</returns>
        public static T[] GetRandomExcept<T>(this T[] sourceArray, int amount, IEnumerable<T> dataValues)
        {
            T[] valuesExcept = sourceArray.Except(dataValues).ToArray();
            return valuesExcept.GetRandomAmount(amount);
        }

        /// <summary>
        /// Calcula o comprimento de um array menos 1, garantindo que o resultado não seja menor que 0.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sequence">Array cujo comprimento será calculado</param>
        /// <returns>Comprimento do array menos 1, não negativo</returns>
        public static int LengthLessOne<T>(this T[] sequence)
        {
            return (int)Mathf.Clamp(sequence.Length - 1, 0, float.MaxValue);
        }

        /// <summary>
        /// Encontra o índice de um item em um array genérico.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="array">Array genérico em que o item será procurado</param>
        /// <param name="item">Item a ser procurado no array</param>
        /// <param name="value">Índice do item encontrado</param>
        /// <returns>Retorna verdadeiro se o item for encontrado no array, falso caso contrário</returns>
        public static bool FindIndex<T>(this T[] array, T item, out int value)
        {
            value = System.Array.FindIndex(array, val => val.Equals(item));
            return value != -1;
        }

        /// <summary>
        /// Esta extensão retorna o índice do primeiro elemento na array que é igual ao item especificado.
        /// </summary>
        /// <typeparam name="T">O tipo de elementos da array</typeparam>
        /// <param name="array">A array na qual será feita a busca</param>
        /// <param name="item">O item que será procurado</param>
        /// <returns>O índice do primeiro elemento igual ao item especificado, ou -1 se o item não for encontrado.</returns>
        public static int FindIndex<T>(this T[] array, T item)
        {
            return System.Array.FindIndex(array, val => val.Equals(item));
        }
    }
}