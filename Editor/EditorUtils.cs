#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gamegaard.Utils
{
    public static class EditorUtils
    {
        public static bool IsPrefabEditor(this GameObject gameObject)
        {
            return gameObject.scene.IsValid() && PrefabStageUtility.GetPrefabStage(gameObject) == null && !PrefabUtility.IsPartOfPrefabAsset(gameObject);
        }

        /// <summary>
        /// Finds a component in any prefab in the project.
        /// </summary>
        /// <typeparam name="T">Type of component to find</typeparam>
        /// <returns>The component if founded, otherwise null</returns>
        public static T FindAssetInPrefab<T>() where T : Component
        {
            string[] guids = AssetDatabase.FindAssets("t:GameObject");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);

#if UNITY_2019_2_OR_NEWER
                if (obj != null && obj.TryGetComponent(out T comp))
                {
                    return comp;
                }
#else
                if (obj != null)
                {
                    T comp = obj.GetComponent<T>();
                    if (comp != null)
                    {
                        return comp;
                    }
                }
#endif
            }

            return null;
        }
    }
}
#endif