using System;

namespace Gamegaard
{
    public static class ReflectionUtils
    {
        public static bool IsOrSubclassOf<T>(this Type type)
        {
            return type == typeof(T) || type.IsSubclassOf(typeof(T));
        }

        public static bool IsSubclassOfGeneric(Type derivedType, Type genericBaseType)
        {
            if (derivedType.IsGenericType && derivedType.GetGenericTypeDefinition() == genericBaseType)
            {
                return true;
            }

            Type baseType = derivedType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericBaseType)
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}