using UnityEngine;

namespace Gamegaard.Utils.Runtime
{
    public static class FloatUtils
    {
        /// <summary>
        /// Retorna o valor limitado a 0.
        /// </summary>
        public static float ClampFloatToZero(float value)
        {
            return Mathf.Clamp(value, 0, float.MaxValue);
        }

        /// <summary>
        /// Retorna o valor com o sinal (positivo ou negativo) de forma randominca.
        /// </summary>
        public static float RandomizeSign(this float value)
        {
            return value *= IntUtils.GetRandomSign();
        }
    }
}