using UnityEditor;
using UnityEngine;

namespace AutoImporterForAseprite
{
    public class AsepriteAutoImporterSettings : ScriptableObject
    {
        [Tooltip(
            @"Exact location of the aseprite executable. On windows it is most likely in 
            C:\Program Files (x86)\Aseprite\Aseprite.exe or in C:\Program Files\Aseprite\Aseprite.exe. 
            On macOS it is most likely in /Applications/Aseprite.app/Contents/MacOS/aseprite")]
        [SerializeField]
        public string pathToAsepriteExecutable;

        public static AsepriteAutoImporterSettings GetSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<AsepriteAutoImporterSettings>(Config.SettingsLocation);
            if (settings) return settings;

            settings = ScriptableObject.CreateInstance<AsepriteAutoImporterSettings>();
            AssetDatabase.CreateAsset(settings, Config.SettingsLocation);

            if (Application.platform == RuntimePlatform.WindowsEditor)
                settings.pathToAsepriteExecutable = @"C:\Program Files\Aseprite\Aseprite.exe";
            else if (Application.platform == RuntimePlatform.OSXEditor)
                settings.pathToAsepriteExecutable = @"/Applications/Aseprite.app/Contents/MacOS/aseprite";

            settings.Save();
            return settings;
        }

        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}