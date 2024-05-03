using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

using UnityEngine;

namespace AutoImporterForAseprite
{
    [UnityEditor.AssetImporters.ScriptedImporter(1, new[] { "ase", "aseprite" })]
    public class AsepriteAutoImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public ImportOptions importOptions;
        public SpriteImportOptions spriteImportOptions;
        public TextureCreationOptions textureCreationOptions;
        public ColabOptions colabOptions;

        [HideInInspector] public List<NamedAnimationOption> animationOptions;
        [HideInInspector] public AseSheetData sheetData;


        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
        {
            var aseWorker = new AseImportContextWorker(ctx)
            {
                TextureCreationOptions = textureCreationOptions,
                SpriteImportOptions = spriteImportOptions
            };

            //Check if this is a ase file. If it's not, we have no business here
            if (!aseWorker.ContextFileIsAseFile())
                return;

            var ase = new AsepriteCli(AsepriteAutoImporterSettings.GetSettings().pathToAsepriteExecutable, importOptions);

            var tempDir = GetTempFolder();
            var sheetDataFile = $"{tempDir}/{aseWorker.AseFileNoExt}_data.json".Replace(" ", "");

            if (animationOptions == null)
            {
                animationOptions = new List<NamedAnimationOption>();
            }
            
            
            var animationOptionsDictionary = animationOptions.ToDictionary(e => e.tagName, e => e.animationOptions);

            if (colabOptions.exportToFile)
            {
                var exportDir = GetExportDir(aseWorker.AseFileNoExt);

                var sheetFile = $"{exportDir}/{aseWorker.AseFileNoExt}_sheet.png";

                ase.ExportSpriteSheet(aseWorker.AseFile, sheetFile, sheetDataFile);

                sheetData = AseSheetData.Create(sheetDataFile, aseWorker.AseFile);

                RefreshAnimationOptions();

                var sheetAssetPath = "Assets/" + sheetFile.Split(new[] { "Assets/" }, StringSplitOptions.RemoveEmptyEntries)[1];

                AssetDatabase.ImportAsset(sheetAssetPath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);

                aseWorker.MainTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(sheetAssetPath);

                if (!Directory.Exists(Path.GetDirectoryName(sheetAssetPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(sheetAssetPath));

                void GenerateSheetLocal()
                {
                    GenerateSheet(aseWorker, sheetAssetPath, animationOptionsDictionary);
                    EditorApplication.delayCall -= GenerateSheetLocal;
                }

                EditorApplication.delayCall += GenerateSheetLocal;
            }
            else
            {
                var sheetFile = $"{tempDir}/{aseWorker.AseFileNoExt}_sheet.png";

                ase.ExportSpriteSheet(aseWorker.AseFile, sheetFile, sheetDataFile);
                sheetData = AseSheetData.Create(sheetDataFile, aseWorker.AseFile);

                aseWorker.AddMainTextureToContext(sheetFile);

                RefreshAnimationOptions();

                aseWorker.AddIndividualSpritesToContext(sheetData);
                aseWorker.AddAnimationsToContext(sheetData, animationOptionsDictionary);
            }

            Directory.Delete(tempDir, true);

            AssetDatabase.SaveAssets();
        }

        private void GenerateSheet(AseImportContextWorker aseWorker, string sheetAssetPath, Dictionary<string, AnimationOptions> animationOptionsDictionary)
        {
            var sheetImporter = GetAtPath(sheetAssetPath) as TextureImporter;
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(sheetAssetPath);

            sheetImporter.wrapMode = textureCreationOptions.textureWrapMode;
            sheetImporter.filterMode = textureCreationOptions.filterMode;
            sheetImporter.alphaIsTransparency = textureCreationOptions.alphaIsTransparency;
            sheetImporter.spriteImportMode = SpriteImportMode.Multiple;


            var sprites = AseImportContextWorker.EnumerateSprites(texture, sheetData, spriteImportOptions).ToArray();

            sheetImporter.spritesheet = sprites.Select(s => new SpriteMetaData
            {
                rect = s.rect,
                name = s.name,
            }).ToArray();

            sheetImporter.SaveAndReimport();


            foreach (var metaFrameTag in sheetData.meta.frameTags)
            {
                var anim = aseWorker.GenerateAnimation(metaFrameTag, sheetData.frames, sprites,
            animationOptionsDictionary?.ContainsKey(metaFrameTag.name) == true
                ? animationOptionsDictionary[metaFrameTag.name]
                : AseImportContextWorker.GetDefaultAnimationOptions);


                if (anim != null)
                {
                    AssetDatabase.CreateAsset(anim, Path.GetDirectoryName(sheetAssetPath) + "/anim_" + metaFrameTag.name + ".anim");
                }
            }
        }

        private void RefreshAnimationOptions()
        {
            if (sheetData.meta.frameTags == null) return;
            var sheetDataFrameTags = new HashSet<string>();
            if (animationOptions == null)
                animationOptions = new List<NamedAnimationOption>();

            foreach (var metaFrameTag in sheetData.meta.frameTags)
            {
                sheetDataFrameTags.Add(metaFrameTag.name);
                var options = animationOptions.Find(a => a.tagName == metaFrameTag.name);
                if (options == null)
                {
                    options = new NamedAnimationOption
                    {
                        animationOptions = AseImportContextWorker.GetDefaultAnimationOptions,
                        tagName = metaFrameTag.name
                    };
                    animationOptions.Add(options);
                }

                if (!options.animationOptions.overrideDirection)
                {
                    if (metaFrameTag.direction == FrameTag.DirForward)
                    {
                        options.animationOptions.direction = Direction.Forward;
                    }
                    else if (metaFrameTag.direction == FrameTag.DirReverse)
                    {
                        options.animationOptions.direction = Direction.Reverse;
                    }
                    else if (metaFrameTag.direction == FrameTag.DirPingPong)
                    {
                        options.animationOptions.direction = Direction.PingPong;
                    }
                }
            }


            foreach (var key in animationOptions.Where(key => !sheetDataFrameTags.Contains(key.tagName)).ToList())
                animationOptions.Remove(key);
        }


        private static string GetTempFolder()
        {
            var dir = Path.Combine(AseUtils.TempFolder, "asetempexports").Replace("\\", "/");
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
            return dir;
        }

        private string GetExportDir(string filename)
        {
            var configDir = colabOptions.exportDirectory;

            if (configDir.Contains("{filename}"))
            {
                configDir = configDir.Replace("{filename}", filename);
            }

            if (string.IsNullOrWhiteSpace(configDir))
            {
                configDir = "";
            }

            string path = Path.Combine(Application.dataPath, configDir).Replace("\\", "/");


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }

    [Serializable]
    public class NamedAnimationOption
    {
        public string tagName;
        public AnimationOptions animationOptions;
    }
}