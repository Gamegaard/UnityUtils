using System.Linq;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class BoolUtils
    {
        /// <summary>
        /// Retorna uma booleana com valor aleatório.
        /// </summary>
        public static bool GetRandomBoolean()
        {
            return Random.value > 0.5f;
        }

        /// <summary>
        /// Retorna True caso o valor passado seja maior ou igual a um valor randomico entre 0 a 1.
        /// </summary>
        public static bool CheckChance(float value)
        {
            return value >= Random.value;
        }

        /// <summary>
        /// Retorna uma soma de booleanas.
        /// </summary>
        public static int CountTrueValues(bool value = true, params bool[] args)
        {
            return args.Count(t => t == value);
        }
    }
}