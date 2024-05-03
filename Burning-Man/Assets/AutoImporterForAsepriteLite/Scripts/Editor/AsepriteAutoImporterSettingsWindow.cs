using System;
using System.IO;
using AutoImporterForAseprite;
using UnityEditor;
using UnityEngine;

namespace AsepriteAutoImporter.Editor
{
    public class AsepriteAutoImporterSettingsWindow : EditorWindow
    {
        private AsepriteAutoImporterSettings settings;

        [MenuItem("Window/Aseprite Auto Importer Settings")]
        private static void ShowWindow()
        {
            var window = GetWindow<AsepriteAutoImporterSettingsWindow>();
            window.titleContent = new GUIContent("Aseprite Auto Importer Settings");
            window.settings = AsepriteAutoImporterSettings.GetSettings();
            window.Show();
        }


        private void OnGUI()
        {
            if (!settings)
                settings = AsepriteAutoImporterSettings.GetSettings();

            EditorGUILayout.LabelField(new GUIContent("Path to aseprite executable",
                @"Exact location of the aseprite executable. On windows it is most likely in 
C:\Program Files (x86)\Aseprite\Aseprite.exe or in C:\Program Files\Aseprite\Aseprite.exe. 
On macOS it is most likely in /Applications/Aseprite.app/Contents/MacOS/aseprite"), EditorStyles.boldLabel);

            settings.pathToAsepriteExecutable = EditorGUILayout.TextArea(settings.pathToAsepriteExecutable);

            EditorGUILayout.Space();

            var fileExists = File.Exists(settings.pathToAsepriteExecutable);

            if (string.IsNullOrEmpty(settings.pathToAsepriteExecutable) || !fileExists)
            {
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    EditorGUILayout.LabelField("Suggested paths:");
                    EditorGUILayout.TextField(@"C:\Program Files\Aseprite\Aseprite.exe");
                    EditorGUILayout.LabelField("or");
                    EditorGUILayout.TextField(@"C:\Program Files (x86)\Aseprite\Aseprite.exe");
                    EditorGUILayout.LabelField("or");
                    EditorGUILayout.TextField(@"C:\Program Files (x86)\Steam\steamapps\common\Aseprite\Aseprite.exe");
                    EditorGUILayout.LabelField("or");
                    EditorGUILayout.TextField(@"C:\Program Files\Steam\steamapps\common\Aseprite\Aseprite.exe");
                }
                else if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    EditorGUILayout.LabelField("Suggested paths:");
                    EditorGUILayout.TextField("/Applications/Aseprite.app/Contents/MacOS/aseprite");
                    EditorGUILayout.LabelField("or");
                    EditorGUILayout.TextField(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "/Library/Application Support/Steam/steamapps/common/Aseprite/aseprite"));
                }
            }

            if (Config.IsLiteVersion)
            {
                EditorGUILayout.LabelField(
                    "Get PRO version of Auto Importer for Aseprite to get automated animation imports!",
                    EditorStyles.boldLabel);
                if (GUILayout.Button("Get PRO"))
                    Application.OpenURL(Config.ProVersionUrl);
            }

            EditorGUILayout.Space();

            if (!fileExists)
            {
                EditorGUILayout.HelpBox("Error! Executable file is not found. Please review the path",
                    MessageType.Error);
            }

            if (GUI.changed)
                settings.Save();
        }
    }
}