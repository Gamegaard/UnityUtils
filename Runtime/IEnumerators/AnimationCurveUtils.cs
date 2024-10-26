using UnityEngine;

namespace Gamegaard.Utils
{
    public static class AnimationCurveUtils
    {
        /// <summary>
        /// Gets the maximum value from the AnimationCurve by checking all keys.
        /// </summary>
        public static int GetMaxValueFromCurve(this AnimationCurve curve)
        {
            float maxValue = float.MinValue;
            foreach (Keyframe key in curve.keys)
            {
                if (key.value > maxValue)
                {
                    maxValue = key.value;
                }
            }
            return Mathf.RoundToInt(maxValue);
        }

        /// <summary>
        /// Gets the minimum value from the AnimationCurve by checking all keys.
        /// </summary>
        public static int GetMinValueFromCurve(this AnimationCurve curve)
        {
            float minValue = float.MaxValue;
            foreach (Keyframe key in curve.keys)
            {
                if (key.value < minValue)
                {
                    minValue = key.value;
                }
            }
            return Mathf.RoundToInt(minValue);
        }
    }
}