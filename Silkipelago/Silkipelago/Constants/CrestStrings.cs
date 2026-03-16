using System.Collections.Generic;

namespace Silkipelago.Constants
{
    public static class CrestStrings
    {
        //crests
        public const string HUNTER = "Hunter";
        public const string REAPER = "Reaper";
        public const string WANDERER = "Wanderer";
        public const string BEAST = "Warrior";
        public const string WITCH = "Witch";
        public const string ARCHITECT = "Toolmaster";
        public const string SHAMAN = "Spell";

        public static readonly List<string> CRESTS = new()
        {
            HUNTER,
            REAPER,
            WANDERER,
            BEAST,
            WITCH,
            ARCHITECT,
            SHAMAN
        };

        //upgrades
        public const string HUNTER_2 = "Hunter_v2";
        public const string HUNTER_3 = "Hunter_v3";

        public static readonly List<string> CRESTS_UPGRADE = new()
        {
            HUNTER_2,
            HUNTER_3
        };
    }
}
