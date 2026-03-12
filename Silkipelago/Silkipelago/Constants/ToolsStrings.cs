using System.Collections.Generic;

namespace Silkipelago.Constants
{
    public static class ToolsStrings
    {
        //silk abilities
        public const string SILK_SPEAR = "Silk Spear";
        public const string THREAD_STORM = "Thread Sphere";
        public const string CROSS_STITCH = "Parry";
        public const string SHARP_DART = "Silk Charge";
        public const string RUNE_RAGE = "Silk Bomb";
        public const string PALE_NAILS = "Silk Boss Needle";

        public static readonly List<string> SILK_ABILITIES = new()
        {
            SILK_SPEAR,
            THREAD_STORM,
            CROSS_STITCH,
            SHARP_DART,
            RUNE_RAGE,
            PALE_NAILS
        };
    }
}
