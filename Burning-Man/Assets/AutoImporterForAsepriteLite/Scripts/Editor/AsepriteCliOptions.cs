using System;

namespace AutoImporterForAseprite
{
    public static class AsepriteCliOptions
    {
        public static string GetValue(this DitheringAlgorithmType type)
        {
            switch (type)
            {
                case DitheringAlgorithmType.NotSet:
                    return "";
                case DitheringAlgorithmType.None:
                    return "none";
                case DitheringAlgorithmType.Ordered:
                    return "ordered";
                case DitheringAlgorithmType.Old:
                    return "old";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetValue(this DitheringMatrixType type)
        {
            switch (type)
            {
                case DitheringMatrixType.NotSet:
                    return "";
                case DitheringMatrixType.Bayer8X8:
                    return "bayer8x8";
                case DitheringMatrixType.Bayer4X4:
                    return "bayer4x4";
                case DitheringMatrixType.Bayer2X2:
                    return "bayer2x2";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetValue(this ColorMode type)
        {
            switch (type)
            {
                case ColorMode.NotSet:
                    return "";
                case ColorMode.Rgb:
                    return "rgb";
                case ColorMode.Grayscale:
                    return "grayscale";
                case ColorMode.Indexed:
                    return "indexed";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public static string GetValue(this SheetType type)
        {
            switch (type)
            {
                case SheetType.NotSet:
                    return "";
                case SheetType.Horizontal:
                    return "horizontal";
                case SheetType.Vertical:
                    return "vertical";
                case SheetType.Rows:
                    return "rows";
                case SheetType.Columns:
                    return "columns";
                case SheetType.Packed:
                    return "packed";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum DitheringAlgorithmType
    {
        NotSet,
        None,
        Ordered,
        Old,
    }


    public enum DitheringMatrixType
    {
        NotSet,
        Bayer8X8,
        Bayer4X4,
        Bayer2X2
    }

    public enum ColorMode
    {
        NotSet,
        Rgb,
        Grayscale,
        Indexed
    }

    public enum SheetType
    {
        NotSet,
        Horizontal,
        Vertical,
        Rows,
        Columns,
        Packed,
    }
}