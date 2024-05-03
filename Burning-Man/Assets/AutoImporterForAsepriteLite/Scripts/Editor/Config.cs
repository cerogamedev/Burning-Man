namespace AutoImporterForAseprite
{
    public static class Config
    {
        public static bool IsLiteVersion = true;

        public static string AssetDir =
            IsLiteVersion ? "Assets/AutoImporterForAsepriteLite/" : "Assets/AutoImporterForAsepritePro/";

        public static string SettingsLocation = AssetDir + "Assets/settings.asset";
        public static string IconPath = AssetDir + "Assets/icon.png";
        public static string ProVersionUrl = "http://u3d.as/1Xt2";
    }
}