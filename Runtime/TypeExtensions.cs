using System;

namespace Gamegaard.Utils
{
    public static class TypeExtensions
    {
        public static bool IsOrSubclassOf<T>(this Type type)
        {
            return type == typeof(T) || type.IsSubclassOf(typeof(T));
        }
    }
}