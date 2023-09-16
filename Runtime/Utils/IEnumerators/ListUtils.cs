using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils.Runtime
{
    public static class ListUtils
    {
        #region List
        /// <summary>
        /// Obtém um elemento aleatório de uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sequence">Lista de elementos</param>
        /// <returns>Retorna um elemento aleatório da lista ou o valor padrão do tipo T se estiver vazia.</returns>
        public static T GetRandom<T>(this List<T> sequence)
        {
            return sequence.Count > 0 ? sequence[Random.Range(0, sequence.Count)] : default;
        }

        /// <summary>
        /// Obtém uma quantidade aleatória de elementos de uma lista.
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="sequence">Lista de elementos</param>
        /// <param name="amount">Quantidade de elementos aleatórios a serem obtidos</param>
        /// <returns>Retorna uma lista de elementos aleatórios</returns>
        public static List<T> GetRandomAmount<T>(this List<T> sequence, int amount)
        {
            System.Random rnd = new System.Random();
            int realAmount;
            if (sequence.Count < amount)
            {
                realAmount = sequence.Count;
            }
            else
            {
                Debug.LogWarning("GetRandom with reduced amount. The list has less elements than required.");
                realAmount = amount;
            }

            return (sequence.Count > 0) ? sequence.OrderBy(x => rnd.Next()).Take(realAmount).ToList() : default;
        }

        /// <summary>
        /// Obtém o valor inteiro da contagem da lista menos um, usando a função Clamp do Mathf para garantir que o resultado seja sempre entre 0 e float.MaxValue.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sequence">A lista a ser contada.</param>
        /// <returns>Retorna o valor inteiro da contagem da lista menos um, nunca negativo</returns>
        public static int CountLessOne<T>(this List<T> sequence)
        {
            return (int)Mathf.Clamp(sequence.Count - 1, 0, float.MaxValue);
        }

        /// <summary>
        /// Verifica se a lista atingiu sua capacidade máxima de elementos.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sequence">A lista a ser verificada.</param>
        /// <returns>Retorna true se a lista estiver cheia</returns>
        public static bool IsFull<T>(this List<T> sequence)
        {
            return sequence.Count == sequence.Capacity;
        }

        /// <summary>
        /// Obtém o espaço restante na capacidade da lista.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento da lista.</typeparam>
        /// <param name="sequence">A lista a ser verificada.</param>
        /// <returns>Retorna o espaço restante na capacidade da lista.</returns>
        public static int RemainingSpace<T>(this List<T> sequence)
        {
            return sequence.Capacity - sequence.Count;
        }

        /// <summary>
        /// Ordena uma lista de forma aleatória.
        /// </summary>
        /// <typeparam name="T">Tipo genérico da lista.</typeparam>
        /// <param name="pile">Lista a ser embaralhada.</param>
        /// <returns>Retorna uma nova lista com os elementos embaralhados.</returns>
        public static List<T> ShuffledOrder<T>(this List<T> pile)
        {
            System.Random random = new System.Random();
            return pile.OrderBy(x => random.Next()).ToList();
        }

        /// <summary>
        /// Combina duas listas em uma nova.
        /// </summary>
        /// <typeparam name="T">O tipo de elemento das listas a serem combinadas.</typeparam>
        /// <param name="listA">A primeira lista a ser combinada.</param>
        /// <param name="listB">A segunda lista a ser combinada.</param>
        /// <returns>Retorna uma nova Lista com os elementos de A e B.</returns>
        public static List<T> Combine<T>(this List<T> listA, List<T> listB)
        {
            int arraySize = listA.Count + listB.Count;
            T[] combined = new T[arraySize];

            listA.CopyTo(combined);
            listB.CopyTo(combined, listA.Count);
            return new List<T>(combined);
        }
        #endregion
    }
}