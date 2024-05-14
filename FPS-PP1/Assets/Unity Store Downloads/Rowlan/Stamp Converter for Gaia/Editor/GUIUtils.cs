using UnityEditor;
using UnityEngine;

namespace Rowlan.Converter.Gaia
{
    /// <summary>
    /// Utilities for the Unity inspector
    /// </summary>
    public class GUIUtils
    {
        /// <summary>
        /// Show property for asset path and a selection button for the asset path selection
        /// </summary>
        /// <param name="property"></param>
        /// <param name="title"></param>
        public static void AssetPathSelector(SerializedProperty property, string title)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(property);

                if (GUILayout.Button("Select", GUILayout.Width(60)))
                {
                    string selectedPath = SelectAssetPath(title);
                    if (selectedPath != null)
                    {
                        property.stringValue = selectedPath;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        public static string AssetPathSelector(string path, string title)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel(title);
                path = EditorGUILayout.TextField(path);

                if (GUILayout.Button("Select", GUILayout.Width(60)))
                {
                    string selectedPath = SelectAssetPath(title);
                    if (selectedPath != null)
                    {
                        path = selectedPath;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            return path;
        }

        /// <summary>
        /// Open folder dialog and have user select an asset path
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string SelectAssetPath(string title)
        {
            string selectedPath = EditorUtility.OpenFolderPanel(title, "Assets", "");

            if (!string.IsNullOrEmpty(selectedPath))
            {
                string assetPath = Application.dataPath.Substring("Assets/".Length);
                bool isAssetPath = selectedPath.StartsWith(Application.dataPath);
                if (isAssetPath)
                {
                    string newPath = selectedPath.Substring(assetPath.Length + 1); // +1 for the initial path separator
                    return newPath;
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Path must be in the Assets folder", "Ok");
                }
            }

            return null;
        }
    }
}