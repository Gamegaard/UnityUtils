using System;

namespace Gamegaard.Utils
{
    public static class ParseUtils
    {
        public static float ParseFloat(string text, float defaultValue)
        {
            return float.TryParse(text, out float result) ? result : defaultValue;
        }

        public static int ParseInt(string text, int defaultValue)
        {
            return int.TryParse(text, out int result) ? result : defaultValue;
        }

        public static bool ParseBool(string text, bool defaultValue)
        {
            return bool.TryParse(text, out bool result) ? result : defaultValue;
        }

        public static double ParseDouble(string text, double defaultValue)
        {
            return double.TryParse(text, out double result) ? result : defaultValue;
        }

        public static DateTime ParseDateTime(string text, DateTime defaultValue)
        {
            return DateTime.TryParse(text, out DateTime result) ? result : defaultValue;
        }

        public static TEnum ParseEnum<TEnum>(string text, TEnum defaultValue) where TEnum : struct
        {
            return Enum.TryParse(text, out TEnum result) ? result : defaultValue;
        }
    }
}