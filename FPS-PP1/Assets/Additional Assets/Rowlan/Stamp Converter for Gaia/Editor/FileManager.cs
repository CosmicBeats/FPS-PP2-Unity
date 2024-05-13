using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Rowlan.Converter.Gaia
{
    public class FileManager
    {
        /// <summary>
        /// Get all textures at the specified input path
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public static Texture2D[] GetHeightmaps(string inputPath)
        {
            string[] folders = new string[] { inputPath };

            // get all textures of the input path
            Texture2D[] heightmaps = AssetDatabase.FindAssets($"t: {typeof(Texture2D).Name}", folders)
                        .Select(AssetDatabase.GUIDToAssetPath)
                        .Select(AssetDatabase.LoadAssetAtPath<Texture2D>)
                        .ToArray();

            // sort by name
            heightmaps = heightmaps.OrderBy(go => go.name).ToArray();

            return heightmaps;
        }
    }
}