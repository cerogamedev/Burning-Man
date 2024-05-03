using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AutoImporterForAseprite
{
    public class AsepriteCli
    {
        public const string DefaultFileNameFormat = "{title}_{tag}_frame{tagframe1}";

        /// <summary>
        /// The path to the Aseprite executable
        /// </summary>
        private readonly string executablePath;

        public ImportOptions importOptions;


        public AsepriteCli(string executablePath, ImportOptions importOptions)
        {
            this.executablePath = executablePath;
            this.importOptions = importOptions ?? new ImportOptions();

            if (!File.Exists(executablePath))
            {
                throw new FileNotFoundException(
                    "Aseprite executable not found. Set it in the settings menu (Window -> Aseprite Auto Importer Settings)");
            }
        }


        public string Execute(string arguments)
        {
            var startInfo = new ProcessStartInfo(executablePath)
            {
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = Process.Start(startInfo);

            if (process == null) throw new Exception("Error while starting Aseprite process");
            process.WaitForExit();

            return process.StandardOutput.ReadToEnd();

        }

        public void ExportSpriteSheet(string aseFile, string sheetOutPath, string sheetDataOutPath, int scale = -1)
        {
            if (scale < 1)
                scale = importOptions.scale;

            if (!sheetOutPath.EndsWith(".png"))
                sheetOutPath += ".png";


            if (!sheetDataOutPath.EndsWith(".json"))
                sheetDataOutPath += ".json";

            var argumentsSb = new StringBuilder();

            argumentsSb.Append(" -b");

            if (importOptions.splitLayers)
                argumentsSb.Append(" --split-layers");

            if (importOptions.splitSlices)
                argumentsSb.Append(" --list-slices");

            argumentsSb.Append(" \"").Append(aseFile).Append('\"');
            argumentsSb.Append(" --scale ").Append(scale);

            if (importOptions.ditheringAlgorithmType != DitheringAlgorithmType.NotSet)
            {
                argumentsSb.Append(" --dithering-algorithm ")
                    .Append(importOptions.ditheringAlgorithmType.GetValue());
            }

            if (importOptions.ditheringMatrixType != DitheringMatrixType.NotSet)
                argumentsSb.Append(" --dithering-matrix ").Append(importOptions.ditheringMatrixType.GetValue());

            if (importOptions.colorMode != ColorMode.NotSet)
                argumentsSb.Append(" --color-mode ").Append(importOptions.colorMode.GetValue());


            if (!string.IsNullOrEmpty(importOptions.fileNameFormat))
            {
                var ff = importOptions.fileNameFormat;
                //Make sure the layer is contained in the name in case we split layers. Otherwise we might get duplicate names
                if (importOptions.splitLayers && !ff.Contains("{layer}"))
                {
                    Debug.LogWarning(
                        "Aseprite Importer: You have specified the split layers option, but haven't specified a file name format that includes the {layer} tag. This can result in duplicate file names. The importer added the {layer} tag at the end of your file name format to prevent that");
                    ff += "_{layer}";
                }

                argumentsSb.Append(" --filename-format \"").Append(ff).Append('\"');
            }

            argumentsSb.Append(" --list-tags --sheet \"").Append(sheetOutPath).Append('\"');
            argumentsSb.Append(" --format json-array --data \"").Append(sheetDataOutPath).Append('\"');
            Execute(argumentsSb.ToString());
        }

        public IEnumerable<string> ListLayers(string aseFile)
        {
            var newLines = new[] { Environment.NewLine, "\\n\\r", "\\r\\n", "\\n" };
            return Execute("-b --list-layers " + aseFile).Split(newLines, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    [Serializable]
    public class ImportOptions
    {
        [Tooltip(
            "Adjust this to generated scaled sprites. E.g. with a scale of 2, the generated sprites will be 2x bigger than the source")]
        public int scale = 1;

        [Tooltip("Enable this to export layers individually. Note! Does not work well with animations or slices split")]
        public bool splitLayers;

        [Tooltip("Enable this to export slices individually. Note! Does not work well with animations or layers split")]
        public bool splitSlices;

        [Tooltip(@"
This option specifies the special string used to format generated filenames 
The FORMAT string can contain some special values:
    - {fullname}: Original sprite full filename (path + file + extension).
    - {path}: Path of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be C:\game-assets.
    - {name}: Name (including extension) of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be file.ase.
    - {title}: Name without extension of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be file.
    - {extension}: Extension of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be ase.
    - {layer}: Current layer name.
    - {tag}: Current tag name.
    - {innertag}: Smallest/inner current tag name.
    - {outertag}: Largest/outer current tag name.
    - {frame}: Current frame (starting from 0). You can use {frame1} to start from 1, or other formats like {frame000}, or {frame001}, etc.
    - {tagframe}: The current frame in the current tag. It's 0 for the first frame of the tag, and so on. Same as {frame}, it accepts variants like {tagframe000}.
            ")]
        public string fileNameFormat = AsepriteCli.DefaultFileNameFormat;

        [Tooltip("Dithering algorithm used in indexed color mode to convert images from RGB to Indexed.")]
        public DitheringAlgorithmType ditheringAlgorithmType = DitheringAlgorithmType.NotSet;

        [Tooltip(
            "Dithering matrix used for the dithering algorithm in indexed color mode to convert images from RGB to Indexed. ")]
        public DitheringMatrixType ditheringMatrixType = DitheringMatrixType.NotSet;

        [Tooltip("Changes the color mode to the given mode of exported sprites")]
        public ColorMode colorMode = ColorMode.NotSet;
    }
}