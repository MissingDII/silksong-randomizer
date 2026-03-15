using System.Collections.Generic;

namespace Silkipelago.Constants
{
    public static class CollectablesStrings
    {
        // Tool and Crest Upgrades
        public const string MEMORY_LOCKET = "Crest Socket Unlocker";
        public const string CRAFT_METAL = "Tool Metal";

        public static readonly List<string> COLLECTABLESKEYS = new()
        {
            WHITE_KEY,
            WHITE_BOSS_KEY,
            ARCHITECT_KEY
        };

        //Keys
        public const string WHITE_KEY = "Ward Key";
        public const string WHITE_BOSS_KEY = "Ward Boss Key";
        public const string ARCHITECT_KEY = "Architect Key";

        public static readonly List<string> TOOLCRESTUPGRADE = new()
        {
            MEMORY_LOCKET,
            CRAFT_METAL,
        };

        //Items
        public const string TWISTED_BUD = "Wood Witch Item";

        public static readonly List<string> ITEMS = new()
        {
            TWISTED_BUD
        };
    }
}

