using UnityEditor;
using UnityEngine;

namespace Gamegaard.Utils.Editor
{
    public static class SOUtils
    {
        /// <summary>
        /// Confere se é possivel, e Renomeia um ScriptableObject.
        /// </summary>
        public static void RenameScriptableObject(ScriptableObject obj, string newMainName, string[] extraNames)
        {
            if (!string.IsNullOrWhiteSpace(newMainName))
            {
                string finalName = newMainName.Replace(" ", "");

                foreach (string value in extraNames)
                {
                    if (value != null)
                    {
                        finalName += $"_{value}";
                    }
                }

                string assetPath = AssetDatabase.GetAssetPath(obj.GetInstanceID());
                AssetDatabase.RenameAsset(assetPath, finalName);
                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(obj);
            }
        }
    }
}
