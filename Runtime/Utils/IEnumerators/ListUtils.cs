using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class ListUtils
    {
        #region List
        /// <summary>
        /// Obtém um elemento aleatório de uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceList">Lista de elementos</param>
        /// <returns>Retorna um elemento aleatório da lista ou o valor padrão do tipo T se estiver vazia.</returns>
        public static T GetRandom<T>(this List<T> sourceList)
        {
            return sourceList.Count > 0 ? sourceList[Random.Range(0, sourceList.Count)] : default;
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sourceList">Lista de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <returns>Retorna uma lista de elementos aleatórios</returns>
        public static List<T> GetRandomAmount<T>(this List<T> sourceList, int amount)
        {
            System.Random rnd = new System.Random();
            int realAmount;
            if (sourceList.Count < amount)
            {
                realAmount = sourceList.Count;
            }
            else
            {
                Debug.LogWarning("GetRandom with reduced amount. The list has less elements than required.");
                realAmount = amount;
            }

            return (sourceList.Count > 0) ? sourceList.OrderBy(x => rnd.Next()).Take(realAmount).ToList() : default;
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