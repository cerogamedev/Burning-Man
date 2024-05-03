using System.IO;
using UnityEditor;
using UnityEngine;

namespace AutoImporterForAseprite
{
    public static class AseUtils
    {
        private static string _tempFolder;

        public static string TempFolder => string.IsNullOrEmpty(_tempFolder)
            ? _tempFolder = FileUtil.GetUniqueTempPathInProject().Replace("\\", "/")
            : _tempFolder;


      
        public static bool IsAseFile(string path)
        {
            if (!Path.HasExtension(path)) return false;

            var ext = Path.GetExtension(path);
            return ext == ".ase" || ext == ".aseprite";
        }

        public static Texture2D GetPixelArtIcon()
        {
            try
            {
                return AssetDatabase.LoadAssetAtPath<Texture2D>(Config.IconPath);
            }
            catch
            {
                return null;
            }
        }
    }
}