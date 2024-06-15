using UnityEngine;

namespace Gamegaard.Utils
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

        /// <summary>
        /// Retorna em porcentagem o quanto o valor atual vale entre um valor minimo e maximo.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float PercentBtweenValues(float value, float minValue, float maxValue)
        {
            return (value - minValue) / (maxValue - minValue);
        }
    }
}