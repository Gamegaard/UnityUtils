using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Linq;
using Gamegaard.Commons.Editor.Utils;

namespace Gamegaard.Commons.Editor
{
    public abstract class GenericScriptableObjectEditor<T> : EditorWindow where T : ScriptableObject
    {
        protected Vector2 rightPanelScroll;
        protected Vector2 leftPanelScroll;
        protected int selectedIndex = -1;
        protected List<T> trackedObjects;
        protected int selectedTab = 0;
        protected ReorderableList reorderableList;
        protected T currentSelectedObject;

        private static T copiedObject;
        protected abstract string[] Tabs { get; }
        protected abstract string Title { get; }
        protected abstract void DrawDetails(SerializedObject serializedObject);

        protected virtual void OnEnable()
        {
            if (position.width == 0 && position.height == 0)
            {
                minSize = new Vector2(500, 500);
                maxSize = new Vector2(500, 500);
                position = new Rect(position.x, position.y, 500, 500);
            }

            trackedObjects = LoadObjects();
            InitializeReorderableList();
        }

        protected virtual List<T> LoadObjects()
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}")
                .Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();
        }

        private void InitializeReorderableList()
        {
            reorderableList = new ReorderableList(trackedObjects, typeof(T), true, true, true, true);
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, $"{typeof(T).Name}s");
            };
            reorderableList.onAddCallback = (ReorderableList list) =>
            {
                T newObject = CreateInstance<T>();
                newObject.name = $"New {typeof(T).Name}";
                string path = $"Assets/New{typeof(T).Name}.asset";
                AssetDatabase.CreateAsset(newObject, AssetDatabase.GenerateUniqueAssetPath(path));
                AssetDatabase.SaveAssets();
                trackedObjects.Add(newObject);
                list.index = trackedObjects.Count - 1;
                currentSelectedObject = newObject;
                EditorUtility.SetDirty(this);
                AssetDatabase.Refresh();
                Repaint();
            };
            reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                if (list.index >= 0 && list.index < trackedObjects.Count)
                {
                    T objectToRemove = trackedObjects[list.index];
                    string path = AssetDatabase.GetAssetPath(objectToRemove);
                    trackedObjects.RemoveAt(list.index);
                    currentSelectedObject = null;
                    if (!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.DeleteAsset(path);
                    }
                    EditorUtility.SetDirty(this);
                    AssetDatabase.Refresh();
                    Repaint();
                }
            };
            reorderableList.onReorderCallback = (ReorderableList list) =>
            {
                EditorUtility.SetDirty(this);
                AssetDatabase.Refresh();
            };
            reorderableList.onSelectCallback = (ReorderableList list) =>
            {
                selectedIndex = list.index;
                currentSelectedObject = trackedObjects[selectedIndex];
                Repaint();
            };
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (index < trackedObjects.Count && trackedObjects[index] != null)
                {
                    if (Event.current.type == EventType.ContextClick && rect.Contains(Event.current.mousePosition))
                    {
                        Event.current.Use();
                        ShowContextMenu(index);
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), trackedObjects[index].name);
                }
            };
        }

        private void ShowContextMenu(int index)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Duplicate"), false, () => DuplicateObject(index));
            menu.AddItem(new GUIContent("Copy"), false, () => CopyObject(index));
            if (copiedObject != null)
            {
                menu.AddItem(new GUIContent("Paste"), false, () => PasteObject(index));
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste"));
            }
            menu.AddItem(new GUIContent("Ping"), false, () => PingObject(index));
            menu.ShowAsContext();
        }

        private void PingObject(int index)
        {
            EditorGUIUtility.PingObject(trackedObjects[index]);
        }

        private void CopyObject(int index)
        {
            if (index >= 0 && index < trackedObjects.Count)
            {
                copiedObject = trackedObjects[index];
            }
        }

        private void PasteObject(int index)
        {
            if (index >= 0 && index < trackedObjects.Count && copiedObject != null)
            {
                T targetObject = trackedObjects[index];
                string targetPath = AssetDatabase.GetAssetPath(targetObject);
                string targetFileName = System.IO.Path.GetFileNameWithoutExtension(targetPath);

                EditorUtility.CopySerialized(copiedObject, targetObject);

                targetObject.name = targetFileName;

                EditorUtility.SetDirty(targetObject);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Repaint();
            }
        }

        private void DuplicateObject(int index)
        {
            if (index >= 0 && index < trackedObjects.Count)
            {
                T sourceObject = trackedObjects[index];
                T newObject = Instantiate(sourceObject);
                string sourcePath = AssetDatabase.GetAssetPath(sourceObject);
                string directory = System.IO.Path.GetDirectoryName(sourcePath);
                newObject.name = sourceObject.name + " (Copy)";
                string newPath = $"{directory}/{newObject.name}.asset";
                AssetDatabase.CreateAsset(newObject, AssetDatabase.GenerateUniqueAssetPath(newPath));
                AssetDatabase.SaveAssets();
                trackedObjects.Add(newObject);
                reorderableList.index = trackedObjects.Count - 1;
                currentSelectedObject = newObject;
                EditorUtility.SetDirty(this);
                AssetDatabase.Refresh();
                Repaint();
            }
        }

        protected virtual void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayoutOption expandHeight = GUILayout.ExpandHeight(true);

            EditorGUILayout.BeginVertical(GUILayout.Width(200), expandHeight);
            GUIStyle darkBackground = new GUIStyle(GUI.skin.box)
            {
                normal = { background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f)) }
            };

            EditorGUILayout.BeginVertical(darkBackground, expandHeight);
            GamegaardGUIUtils.DrawBigTitleText(Title);
            selectedTab = GUILayout.Toolbar(selectedTab, Tabs);
            EditorGUILayout.Space();
          
            leftPanelScroll = EditorGUILayout.BeginScrollView(leftPanelScroll);
            reorderableList.DoLayoutList();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();

            rightPanelScroll = EditorGUILayout.BeginScrollView(rightPanelScroll, GUILayout.ExpandWidth(true), GUILayout.MaxWidth(position.width - 202), GUILayout.ExpandHeight(true));
           
            if (currentSelectedObject != null)
            {
                SerializedObject serializedObject = new SerializedObject(currentSelectedObject);
                serializedObject.Update();
                DrawDetails(serializedObject);
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
        }

        protected Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
