using System;

namespace EnemyScripts
{
    public static class EnemyUtils
    {
        public static string NameToIcon(GestureName gesture)
        {
            switch (gesture)
            {
                case GestureName.Undefined:
                    return string.Empty;
                case GestureName.HorizontalLine:
                    return "-";
                case GestureName.VerticalLine:
                    return "|";
                case GestureName.UpArrow:
                    return "^";
                case GestureName.DownArrow:
                    return "v";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gesture), gesture, null);
            }
        }
    }
}