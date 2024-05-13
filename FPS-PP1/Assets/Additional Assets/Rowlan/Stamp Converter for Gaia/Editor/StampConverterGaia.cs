using System.IO;
using UnityEditor;
using UnityEngine;

namespace Rowlan.Converter.Gaia
{
    public class StampConverterGaia : EditorWindow
    {
        private string inputPath = "";
        private string stampPath = @"Assets/Gaia User Data/Stamps";
        private string stampFolderName = @"Converted Stamps";
        private bool overwriteExisting = false;

        [MenuItem("Tools/Stamp Converter/Gaia")]
        public static void Open()
        {
            StampConverterGaia window = GetWindow<StampConverterGaia>(false, "Stamp Converter for Gaia", true);
            window.Show();

        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("Convert all Texture2D at an input location to stamps which are ready to be used by the Gaia stamper. The stamps will be saved at the specified loaction.", MessageType.None);

            inputPath = GUIUtils.AssetPathSelector(inputPath, "Input Path");

            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("The input path of the heightmaps. Example: Assets/Rowlan/Terrain/Stamps/Impact/Heightmaps", MessageType.None);
            EditorGUI.indentLevel--;

            stampPath = GUIUtils.AssetPathSelector(stampPath, "Stamp Path");

            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("The output path at which Gaia searches the stamps. Example: Assets/Gaia User Data/Stamps", MessageType.None);
            EditorGUI.indentLevel--;

            stampFolderName = GUIUtils.AssetPathSelector(stampFolderName, "Stamp Folder Name");

            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("The folder in the Gaia stamps folder which will be created and to which the stamps will be converted to. Example: MyStamps\nThis would result in a target folder Assets/Gaia User Data/Stamps/MyStamps", MessageType.None);
            EditorGUI.indentLevel--;

            overwriteExisting = EditorGUILayout.Toggle("Overwrite Existing", overwriteExisting);

            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("If enabled, then existing files will be overwritten. Otherwise they will be skipped", MessageType.None);
            EditorGUI.indentLevel--;

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Convert to Stamps for Gaia", GUILayout.Height(30)))
                {
                    ConvertToGaiaStamps();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private bool PerformConsistencyCheck()
        {
            if( string.IsNullOrEmpty(inputPath) ) 
            {
                EditorUtility.DisplayDialog("Error", "Please provide an input path", "Ok");
                return false;
            }

            if (string.IsNullOrEmpty(stampPath))
            {
                EditorUtility.DisplayDialog("Error", "Please provide a stamp path", "Ok");
                return false;
            }

            return true;
        }

        private void ConvertToGaiaStamps()
        {
            if (!PerformConsistencyCheck())
                return;

            string outputPath = Path.Combine(stampPath, stampFolderName);
            Converter.ConvertToGaiaStamps(inputPath, outputPath, overwriteExisting, true, true);

        }
    }
}