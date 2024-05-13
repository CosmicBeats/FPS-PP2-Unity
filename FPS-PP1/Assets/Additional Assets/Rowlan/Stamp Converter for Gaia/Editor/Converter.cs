using System.IO;
using UnityEditor;
using UnityEngine;

namespace Rowlan.Converter.Gaia
{
    public class Converter
    {
        /// <summary>
        /// Convert the stamps at input path to stamps for Gaia and save them at the output path
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        public static void ConvertToGaiaStamps( string inputPath, string outputPath, bool overwriteExisting, bool showInitialConfirmation, bool showSummary)
        {
            int convertCount = 0;
            int skippedCount = 0;

            try
            {
                // get all heightmaps as textures
                Texture2D[] heightmaps = FileManager.GetHeightmaps(inputPath);

                if(showInitialConfirmation)
                {
                    bool isContinue = EditorUtility.DisplayDialog("Convert to stamps for Gaia", $"{heightmaps.Length} files will be converted. Continue?", "Yes", "No");

                    if (!isContinue)
                        return;
                }

                int index = 0;

                foreach (Texture2D heightmap in heightmaps)
                {
                    Debug.Log($"Processing {heightmap.name}");

                    EditorUtility.DisplayProgressBar("Convert Stamps to Gaia", $"Converting {index+1}/{heightmaps.Length}: {heightmap.name}", (index / (float) heightmaps.Length));
                    index++;

                    #region path
                    string name = Path.GetFileNameWithoutExtension(heightmap.name);

                    string folder = Path.Combine(outputPath);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                        AssetDatabase.Refresh();
                    }

                    string path = Path.Combine(folder, name + ".exr");

                    if( File.Exists(path) && !overwriteExisting) 
                    {
                        Debug.Log($"File exists, skpping: {path}");
                        skippedCount++;
                        continue;
                    }
                    else
                    {
                        convertCount++;
                    }

                    #endregion path

                    int width = heightmap.width;
                    int height = heightmap.height;

                    Texture2D texOut = new Texture2D(heightmap.width, heightmap.height, TextureFormat.RGBAFloat, false, true);

                    var target = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);

                    Graphics.Blit(heightmap, target);

                    var lastActive = RenderTexture.active;
                    RenderTexture.active = target;
                    texOut.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
                    texOut.Apply();
                    RenderTexture.active = lastActive;

                    byte[] bytes = ImageConversion.EncodeToEXR(texOut, /*Texture2D.EXRFlags.OutputAsFloat | */Texture2D.EXRFlags.CompressZIP);

                    File.WriteAllBytes(path, bytes);

                    RenderTexture.ReleaseTemporary(target);
                    UnityEngine.Object.DestroyImmediate(texOut);

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();


                    TextureImporter combinedImporter = (TextureImporter)AssetImporter.GetAtPath(path);
                    combinedImporter.sRGBTexture = false;
                    combinedImporter.mipmapEnabled = false;
                    combinedImporter.wrapMode = TextureWrapMode.Clamp;
                    combinedImporter.maxTextureSize = 4096;
                    combinedImporter.textureCompression = TextureImporterCompression.Uncompressed;

                    TextureImporterPlatformSettings settings = combinedImporter.GetDefaultPlatformTextureSettings();
                    settings.maxTextureSize = 4096;
                    settings.format = TextureImporterFormat.R16;
                    settings.textureCompression = TextureImporterCompression.Uncompressed;

                    combinedImporter.SetPlatformTextureSettings(settings);

                    combinedImporter.SaveAndReimport();

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                }

                AssetDatabase.Refresh();
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (showSummary)
            {
                EditorUtility.DisplayDialog("Convert to stamps for Gaia", $"Conversion complete.\n\nConverted: {convertCount}\nSkipped: {skippedCount}", "Ok");
            }

        }
    }
}