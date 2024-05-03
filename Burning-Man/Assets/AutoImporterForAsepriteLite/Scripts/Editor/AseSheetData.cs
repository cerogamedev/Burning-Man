using System;
using System.IO;
using UnityEngine;

namespace AutoImporterForAseprite
{
    [Serializable]
    public class AseSheetData
    {
        public FrameElement[] frames;
        public Meta meta;
        public string pathToSheetData;
        public string aseFileName;

        public static AseSheetData Create(string sheetDataFile, string aseName)
        {
            var sheetData = JsonUtility.FromJson<AseSheetData>(File.ReadAllText(sheetDataFile));
            sheetData.pathToSheetData = sheetDataFile;
            sheetData.aseFileName = aseName;
            return sheetData;
        }
    }

    [Serializable]
    public class FrameElement
    {
        public string filename;
        public SpriteSourceSizeClass frame;
        public bool rotated;
        public bool trimmed;
        public SpriteSourceSizeClass spriteSourceSize;
        public Size sourceSize;
        public int duration;
    }

    [Serializable]
    public class SpriteSourceSizeClass
    {
        public int x;
        public int y;
        public int w;
        public int h;

        public Rect ToRect()
        {
            return new Rect(x, y, w, h);
        }
    }

    [Serializable]
    public class Size
    {
        public int w;
        public int h;
    }

    [Serializable]
    public class Meta
    {
        public Uri app;
        public string version;
        public string image;
        public string format;
        public Size size;
        public int scale;
        public FrameTag[] frameTags;
        public SliceData[] slices;
    }

    [Serializable]
    public class SliceData
    {
        public string name;
        public string color;
        public SliceKeyData[] keys;
    }

    [Serializable]
    public class SliceKeyData
    {
        public int frame;
        public SpriteSourceSizeClass bounds;
        public SpriteSourceSizeClass center;
        public Pivot pivot;
    }

    [Serializable]
    public class Pivot
    {
        public int x;
        public int y;

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }
    }

    [Serializable]
    public class FrameTag
    {
        public string name;
        public int from;

        /// <summary>
        /// Inclusive
        /// </summary>
        public int to;

        public string direction;

        public const string DirForward = "forward";
        public const string DirReverse = "reverse";
        public const string DirPingPong = "pingpong";
    }
}