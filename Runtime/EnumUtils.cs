using System;

namespace Gamegaard.Utils
{
    public static class EnumUtils
    {
        private static class EnumValueCache<T> where T : Enum
        {
            public static readonly T[] Values = (T[])Enum.GetValues(typeof(T));
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            T[] enumValues = EnumValueCache<T>.Values;
            if (enumValues.Length == 0) throw new InvalidOperationException("Cannot select a value from an empty enum.");

            return enumValues[UnityEngine.Random.Range(0, enumValues.Length)];
        }

        public static T GetRandomEnumValue<T>(Func<T, bool> condition) where T : Enum
        {
            if (condition == null) return GetRandomEnumValue<T>();

            T[] enumValues = EnumValueCache<T>.Values;
            if (enumValues.Length == 0) throw new InvalidOperationException("Cannot select a value from an empty enum.");

            const int maximumRandomAttempts = 16;
            for (int attempt = 0; attempt < maximumRandomAttempts; attempt++)
            {
                T randomValue = enumValues[UnityEngine.Random.Range(0, enumValues.Length)];
                if (condition(randomValue)) return randomValue;
            }

            int matchingCount = 0;
            T selectedValue = default;

            foreach (T enumValue in enumValues)
            {
                if (!condition(enumValue)) continue;

                matchingCount++;
                if (UnityEngine.Random.Range(0, matchingCount) == 0)
                    selectedValue = enumValue;
            }

            if (matchingCount == 0) throw new InvalidOperationException("No enum values satisfy the provided condition.");
            return selectedValue;
        }

        public static T[] GetRandomEnumValueAmount<T>(int amount) where T : Enum
        {
            T[] enumElements = new T[amount];
            for (int i = 0; i < amount; i++)
            {
                enumElements[i] = GetRandomEnumValue<T>();
            }
            return enumElements;
        }

        public static T[] GetRandomEnumValueAmount<T>(int amount, Func<T, bool> condition) where T : Enum
        {
            T[] enumElements = new T[amount];
            for (int i = 0; i < amount; i++)
            {
                enumElements[i] = GetRandomEnumValue(condition);
            }
            return enumElements;
        }
    }
}