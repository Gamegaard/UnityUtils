using UnityEngine;

namespace Gamegaard.Utils
{
    public static class AnimationCurveUtils
    {
        /// <summary>
        /// Gets the maximum value from the AnimationCurve by checking all keys.
        /// </summary>
        public static float GetMaxValueFromCurve(this AnimationCurve curve)
        {
            Keyframe[] keyframes = curve.keys;
            if (keyframes.Length == 0) return 0f;

            float maxValue = float.MinValue;
            for (int i = 0; i < keyframes.Length; i++)
            {
                Keyframe key = keyframes[i];
                if (key.value > maxValue) maxValue = key.value;
            }
            return maxValue;
        }

        /// <summary>
        /// Gets the minimum value from the AnimationCurve by checking all keys.
        /// </summary>
        public static float GetMinValueFromCurve(this AnimationCurve curve)
        {
            Keyframe[] keyframes = curve.keys;
            if (keyframes.Length == 0) return 0f;

            float minValue = float.MaxValue;
            for (int i = 0; i < keyframes.Length; i++)
            {
                Keyframe key = keyframes[i];
                if (key.value < minValue) minValue = key.value;
            }
            return minValue;
        }
    }
}