using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class IEnumeratorUtils
    {
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
                Debug.LogWarning($"GetRandom with reduced amount, from {amount}, to {count}. The list has less elements than required.");
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
    }
}