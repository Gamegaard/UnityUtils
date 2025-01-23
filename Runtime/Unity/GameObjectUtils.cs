using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gamegaard.Utils
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// Checa se o GameObject tem um componente específico.
        /// </summary>
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponent<T>();
        }

        /// <summary>
        /// Ativa ou desativa todos os componentes do GameObject.
        /// </summary>
        public static void SetAllComponentsEnabled(this GameObject go, bool isActive)
        {
            MonoBehaviour[] itemz = go.GetComponents<MonoBehaviour>();
            for (int i = 0; i < itemz.Length; i++)
            {
                MonoBehaviour c = itemz[i];
                c.enabled = isActive;
            }
        }

        /// <summary>
        /// Retorna verdadeiro quando o objeto não é nulo.
        /// </summary>
        public static bool IsAlive(this object aObj)
        {
            if (aObj == null) return false;

            return aObj as Object != null;
        }

        /// <summary>
        /// Returns the component if it is present in the children of the current object. Excludes itself from the search (caster).
        /// </summary>
        /// <param name="caster">The current object.</param>
        /// <param name="includeInactive">Specifies whether the method should include components of inactive objects (default = false).</param>
        /// <typeparam name="T">The type of component to be found.</typeparam>
        /// <returns>The component if found, null Otherwise.</returns>
        public static T GetComponentInChildrenIgnoreSelf<T>(this GameObject caster, bool includeInactive = false) where T : Component
        {
            T[] components = caster.GetComponentsInChildren<T>(includeInactive);

            foreach (T component in components)
            {
                if (component.gameObject != caster)
                {
                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if the specified flag is set in the given value.
        /// </summary>
        /// <param name="flag">The flag to be checked.</param>
        /// <param name="value">The value to check the flag against.</param>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>True if the flag is set, false otherwise.</returns>
        public static bool HasFlag<T>(this T flag, T value) where T : Enum
        {
            int intFlag = Convert.ToInt32(flag);
            int intValue = Convert.ToInt32(value);
            return (intFlag & intValue) != 0;
        }

        /// <summary>
        /// Finds a child with the specified name recursively in the given parent.
        /// </summary>
        /// <param name="parent">The transform to search.</param>
        /// <param name="name">The target child name.</param>
        /// <returns>The child if found, null otherwise.</returns>
        public static Transform FindDeepChild(this Transform parent, string name)
        {
            Transform result = parent.Find(name);
            if (result != null) return result;
            foreach (Transform child in parent)
            {
                result = FindDeepChild(child, name);
                if (result != null) return result;
            }
            return null;
        }

        /// <summary>
        /// Finds a child with the specified name and component type recursively in the given parent.
        /// </summary>
        /// <typeparam name="T">The component type to search for.</typeparam>
        /// <param name="parent">The transform to search.</param>
        /// <param name="name">The target child name.</param>
        /// <returns>The component if found, null otherwise.</returns>
        public static T FindDeepChildAs<T>(this Transform parent, string name) where T : Component
        {
            Transform result = parent.Find(name);
#if UNITY_2019_2_OR_NEWER
            if (result != null && result.TryGetComponent(out T component1)) return component1;
            foreach (Transform child in parent)
            {
                result = FindDeepChild(child, name);
                if (result != null && result.TryGetComponent(out T component2)) return component2;
            }
            return null;
#else
            if (result != null)
            {
                T component1 = result.GetComponent<T>();
                if (component1 != null) return component1;
            }

            foreach (Transform child in parent)
            {
                result = FindDeepChild(child, name);
                if (result != null)
                {
                    T component2 = result.GetComponent<T>();
                    if (component2 != null) return component2;
                }
            }
            return null;        
#endif
        }

        /// <summary>
        /// Searches for a component of type T only among the direct children of the object.
        /// </summary>
        /// <typeparam name="T">The type of the component to search for.</typeparam>
        /// <param name="parent">The parent object whose direct children will be checked.</param>
        /// <returns>The first component of type T found, or null if none is found.</returns>
        public static T GetComponentInDirectChildren<T>(this GameObject parent) where T : Component
        {
            foreach (Transform child in parent.transform)
            {
                T component = child.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// Searches for components of type T only among the direct children of the object.
        /// </summary>
        /// <typeparam name="T">The type of the components to search for.</typeparam>
        /// <param name="parent">The parent object whose direct children will be checked.</param>
        /// <returns>An IEnumerable of components of type T found among the direct children.</returns>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject parent) where T : Component
        {
            foreach (Transform child in parent.transform)
            {
                if (child.TryGetComponent(out T component))
                {
                    yield return component;
                }
            }
        }

        /// <summary>
        /// Searches for components of type T only among the direct children of the object and adds them to a provided list.
        /// </summary>
        /// <typeparam name="T">The type of the components to search for.</typeparam>
        /// <param name="parent">The parent object whose direct children will be checked.</param>
        /// <param name="results">The list to which the components will be added.</param>
        public static void GetComponentsInDirectChildren<T>(this GameObject parent, List<T> results) where T : Component
        {
            foreach (Transform child in parent.transform)
            {
                if (child.TryGetComponent(out T component))
                {
                    results.Add(component);
                }
            }
        }

        public static T GetComponentInDirectChildren<T>(this Transform transform, string childrenName) where T : Component
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childrenName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out T slotView))
                    {
                        return slotView;
                    }
                }
            }

            return null;
        }

        public static T GetComponentInChildren<T>(this Transform transform, string childrenName) where T : Component
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childrenName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out T component))
                    {
                        return component;
                    }
                }

                T result = child.GetComponentInChildren<T>(childrenName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public static T GetComponentInParents<T>(this Transform transform, string parentName) where T : Component
        {
            Transform current = transform.parent;

            while (current != null)
            {
                if (current.name.Equals(parentName, StringComparison.OrdinalIgnoreCase))
                {
                    if (current.TryGetComponent(out T component))
                    {
                        return component;
                    }
                }

                current = current.parent;
            }

            return null;
        }

        public static bool TryGetComponentInDirectChildren<T>(this Transform transform, string childrenName, out T component) where T : Component
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childrenName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out component))
                    {
                        return true;
                    }
                }
            }

            component = null;
            return false;
        }

        public static bool TryGetComponentInChildren<T>(this Transform transform, string childrenName, out T component) where T : Component
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childrenName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out component))
                    {
                        return true;
                    }
                }

                if (child.TryGetComponentInChildren(childrenName, out component))
                {
                    return true;
                }
            }

            component = null;
            return false;
        }

        public static bool TryGetComponentInParents<T>(this Transform transform, string parentName, out T component) where T : Component
        {
            Transform current = transform.parent;

            while (current != null)
            {
                if (current.name.Equals(parentName, StringComparison.OrdinalIgnoreCase))
                {
                    if (current.TryGetComponent(out component))
                    {
                        return true;
                    }
                }

                current = current.parent;
            }

            component = null;
            return false;
        }
    }
}