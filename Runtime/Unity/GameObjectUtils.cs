using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gamegaard.Utils
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// Returns true when the GameObject has a component of type T.
        /// </summary>
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// Enables or disables every MonoBehaviour attached to the GameObject.
        /// </summary>
        public static void SetAllComponentsEnabled(this GameObject gameObject, bool isEnabled)
        {
            MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();
            for (int index = 0; index < components.Length; index++)
            {
                components[index].enabled = isEnabled;
            }
        }

        /// <summary>
        /// Returns true when the object is not null, accounting for Unity's fake-null objects.
        /// </summary>
        public static bool IsAlive(this object target)
        {
            return target != null || target as Object != null;
        }

        /// <summary>
        /// Returns the first component or interface of type T found in the children, excluding the caster itself.
        /// </summary>
        /// <param name="caster">The current object.</param>
        /// <param name="includeInactive">Whether inactive children should be included in the search.</param>
        /// <typeparam name="T">The component or interface type to search for.</typeparam>
        public static T GetComponentInChildrenIgnoreSelf<T>(this GameObject caster, bool includeInactive = false)
        {
            Component[] components = caster.GetComponentsInChildren<Component>(includeInactive);

            foreach (Component component in components)
            {
                if (component.gameObject == caster) continue;
                if (component is T typedComponent) return typedComponent;
            }

            return default;
        }

        /// <summary>
        /// Checks whether the specified flag is set in the given value.
        /// </summary>
        public static bool HasFlag<T>(this T flag, T value) where T : Enum
        {
            int intFlag = Convert.ToInt32(flag);
            int intValue = Convert.ToInt32(value);
            return (intFlag & intValue) != 0;
        }

        /// <summary>
        /// Recursively searches for a child with the given name.
        /// </summary>
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
        /// Recursively searches for a child with the given name and returns its component or interface of type T.
        /// </summary>
        public static T FindDeepChildAs<T>(this Transform parent, string name)
        {
            Transform result = parent.Find(name);
            if (result != null && result.TryGetComponent(out T component)) return component;

            foreach (Transform child in parent)
            {
                result = FindDeepChild(child, name);
                if (result != null && result.TryGetComponent(out T childComponent)) return childComponent;
            }

            return default;
        }

        /// <summary>
        /// Searches for a component or interface of type T only among the direct children of the object.
        /// </summary>
        public static T GetComponentInDirectChildren<T>(this GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.TryGetComponent(out T component)) return component;
            }

            return default;
        }

        /// <summary>
        /// Searches for components or interfaces of type T only among the direct children of the object.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject parent)
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
        /// Searches for components or interfaces of type T only among the direct children of the object and adds them to the provided list.
        /// </summary>
        public static void GetComponentsInDirectChildren<T>(this GameObject parent, List<T> results)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.TryGetComponent(out T component))
                {
                    results.Add(component);
                }
            }
        }

        /// <summary>
        /// Searches for a component or interface of type T among the direct children matching the given name.
        /// </summary>
        public static T GetComponentInDirectChildren<T>(this Transform transform, string childName)
        {
            foreach (Transform child in transform)
            {
                if (!child.name.Equals(childName, StringComparison.OrdinalIgnoreCase)) continue;
                if (child.TryGetComponent(out T component)) return component;
            }

            return default;
        }

        /// <summary>
        /// Recursively searches for a component or interface of type T among children matching the given name.
        /// </summary>
        public static T GetComponentInChildren<T>(this Transform transform, string childName)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out T component)) return component;
                }

                T result = child.GetComponentInChildren<T>(childName);
                if (result != null) return result;
            }

            return default;
        }

        /// <summary>
        /// Walks up the hierarchy searching for a component or interface of type T on a parent matching the given name.
        /// </summary>
        public static T GetComponentInParents<T>(this Transform transform, string parentName)
        {
            Transform current = transform.parent;

            while (current != null)
            {
                if (current.name.Equals(parentName, StringComparison.OrdinalIgnoreCase))
                {
                    if (current.TryGetComponent(out T component)) return component;
                }

                current = current.parent;
            }

            return default;
        }

        /// <summary>
        /// Tries to find a component or interface of type T among the direct children matching the given name.
        /// </summary>
        public static bool TryGetComponentInDirectChildren<T>(this Transform transform, string childName, out T component)
        {
            foreach (Transform child in transform)
            {
                if (!child.name.Equals(childName, StringComparison.OrdinalIgnoreCase)) continue;
                if (child.TryGetComponent(out component)) return true;
            }

            component = default;
            return false;
        }

        /// <summary>
        /// Recursively tries to find a component or interface of type T among children matching the given name.
        /// </summary>
        public static bool TryGetComponentInChildren<T>(this Transform transform, string childName, out T component)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals(childName, StringComparison.OrdinalIgnoreCase))
                {
                    if (child.TryGetComponent(out component)) return true;
                }

                if (child.TryGetComponentInChildren(childName, out component)) return true;
            }

            component = default;
            return false;
        }

        /// <summary>
        /// Walks up the hierarchy trying to find a component or interface of type T on a parent matching the given name.
        /// </summary>
        public static bool TryGetComponentInParents<T>(this Transform transform, string parentName, out T component)
        {
            Transform current = transform.parent;

            while (current != null)
            {
                if (current.name.Equals(parentName, StringComparison.OrdinalIgnoreCase))
                {
                    if (current.TryGetComponent(out component)) return true;
                }

                current = current.parent;
            }

            component = default;
            return false;
        }
    }
}