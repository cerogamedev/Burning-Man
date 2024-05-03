using System.Collections.Generic;
using System.Linq;
using AutoImporterForAseprite;
using UnityEditor;

using UnityEngine;

namespace AsepriteAutoImporter.Editor
{
    [CustomEditor(typeof(AutoImporterForAseprite.AsepriteAutoImporter))]
    public class AsepriteImporterEditor : UnityEditor.AssetImporters.ScriptedImporterEditor
    {
        private Dictionary<string, bool> foldouts;

        protected override void Awake()
        {
            base.Awake();
            foldouts = new Dictionary<string, bool>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var importer = (AutoImporterForAseprite.AsepriteAutoImporter) target;

            var frameTags = importer.sheetData.meta.frameTags;
            if (frameTags != null && frameTags.Length > 0)
            {
                EditorGUILayout.LabelField("Animation options:", EditorStyles.boldLabel);
                if (foldouts == null)
                    foldouts = new Dictionary<string, bool>();
                
                EditorGUI.BeginDisabledGroup(Config.IsLiteVersion);
                foreach (var frameTag in frameTags)
                {
                    if (foldouts.ContainsKey(frameTag.name) == false)
                        foldouts[frameTag.name] = false;
                    foldouts[frameTag.name] = EditorGUILayout.Foldout(foldouts[frameTag.name], frameTag.name, true);
                    if (foldouts[frameTag.name])
                    {
                        var no = importer.animationOptions.FirstOrDefault(a => a.tagName == frameTag.name);
                        if (no == null)
                        {
                            no = new NamedAnimationOption
                            {
                                animationOptions = AseImportContextWorker.GetDefaultAnimationOptions,
                                tagName = frameTag.name
                            };
                            importer.animationOptions.Add(no);
                        }

                        var o = no.animationOptions;

                        o.relativePath = EditorGUILayout.TextField(new GUIContent("Relative Path",
                            @"Path to the game object that contains the Sprite/Image component.   
The relativePath is formatted similar to a pathname, e.g. ""root/spine/leftArm"".
If relativePath is empty it refers to the game object the animation clip is attached to. "), o.relativePath);
                        o.componentType = (ComponentType) EditorGUILayout.EnumPopup(
                            new GUIContent("Target Component Type",
                                "Weather the target component is an Image or a Sprite Renderer"),
                            o.componentType);

                        o.overrideDirection = EditorGUILayout.Toggle(
                            new GUIContent("Override Direction",
                                "Leave this unset to get direction from the animation settings in the ase file"),
                            o.overrideDirection);

                        EditorGUI.BeginDisabledGroup(!o.overrideDirection);
                        o.direction = (Direction) EditorGUILayout.EnumPopup("Direction", o.direction);
                        EditorGUI.EndDisabledGroup();

                        o.loopTime = EditorGUILayout.Toggle("Loop Time", o.loopTime);
                        
                        o.useCustomCurve = EditorGUILayout.Toggle(new GUIContent("Use Custom Curve",
                                "Leave out in order to use the frame durations specified in the ase file. Enable to set a custom curve for the animation timing"),
                            o.useCustomCurve);

                        EditorGUI.BeginDisabledGroup(!o.useCustomCurve);
                        o.customCurve = EditorGUILayout.CurveField("Custom Curve", o.customCurve);
                        EditorGUI.EndDisabledGroup();
                    }

                    
                    if (GUI.changed)
                        EditorUtility.SetDirty(importer);
                }
                EditorGUI.EndDisabledGroup();
                
                
                if (Config.IsLiteVersion)
                {
                    EditorGUILayout.LabelField("Get PRO version of Auto Importer for Aseprite to get automated animation imports!", EditorStyles.boldLabel);
                    if(GUILayout.Button("Get PRO"))
                        Application.OpenURL(Config.ProVersionUrl);
                }

            }
        }
    }
}