using System.Collections.Generic;
using System.Linq;


namespace Silkipelago.Constants
{
    public static class ArchipelagoItemIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {
         //Crest
         { "Progressive Hunter Crest", CrestStrings.HUNTER },
         { "Reaper Crest", CrestStrings.REAPER },
         { "Wanderer Crest Crest", CrestStrings.WANDERER },
         { "Beast Crest", CrestStrings.BEAST },
         { "Architect Crest", CrestStrings.ARCHITECT },
         { "Shaman Crest", CrestStrings.SHAMAN },
         { "Witch Crest", CrestStrings.WITCH },

        // items
         { "Twisted Bud", CollectablesStrings.TWISTED_BUD },
        // needolin and melodies 
        { "Needolin", PlayerDataStrings.HAS_NEEDOLIN },
        { "Beastling Call",PlayerDataStrings.BEASTLING_CALL  },
        { "Elegy of the Deep", PlayerDataStrings.ELEGY_OF_THE_DEEP },
        { "Conductor's Melody", PlayerDataStrings.CONDUCTOR_MELODY },
        { "Architect's Melody", PlayerDataStrings.ARCHITECT_MELODY },
        { "Vaultkeeper's Melody", PlayerDataStrings.VAULTKEEPER_MELODY },
        //Tools
        { "Silkspear", ToolsStrings.SILK_SPEAR },
        { "Thread Storm",ToolsStrings.THREAD_STORM  },
        { "Sharpdart", ToolsStrings.SHARP_DART },
        { "Pale Nails", ToolsStrings.PALE_NAILS },
        { "Cross Stitch", ToolsStrings.CROSS_STITCH },
        { "Rune Rage", ToolsStrings.RUNE_RAGE },
        //Shrines
        { "Grand Gate - Marrow Bell", PlayerDataStrings.SHRINE_BONE },
        { "Grand Gate - Deep Docks Bell", PlayerDataStrings.SHRINE_WILDS },
        { "Grand Gate - Shellwood Bell", PlayerDataStrings.SHRINE_SHELLWOOD },
        { "Grand Gate - Greymoor Bell", PlayerDataStrings.SHRINE_GREYMOOR },
        { "Grand Gate - Bellhart Bell", PlayerDataStrings.SHRINE_BELLHART },
        // TODO not added by alex yet
        { "Ring The Bell In Songclave", PlayerDataStrings.SHRINE_ENCLAVE },
        //keys
        { "Key of Indolent", PlayerDataStrings.INDOLENT_KEY },
        { "Key of Heretic", PlayerDataStrings.HERETIC_KEY },
        { "Key of Apostate", PlayerDataStrings.APOSTATE_KEY },
        { "Architect Key", CollectablesStrings.ARCHITECT_KEY },
        { "White Key", CollectablesStrings.WHITE_KEY },
        { "Surgeon's Key", CollectablesStrings.WHITE_BOSS_KEY },
        // Crest Upgrades
        { "Memory Locket", CollectablesStrings.MEMORY_LOCKET },
        { "Craft Metal", CollectablesStrings.CRAFT_METAL },
        //abilities
        { "Drifter's Cloak", PlayerDataStrings.HAS_DRIFTER_CLOAK },
        { "Cling Grip", PlayerDataStrings.HAS_WALL_JUMP },
        { "Swift Step", PlayerDataStrings.HAS_DASH },
        { "Faydown Cloak", PlayerDataStrings.HAS_DOUBLE_JUMP },
        { "Clawline", PlayerDataStrings.HAS_HARPOON_DASH },
        { "Silk Soar", PlayerDataStrings.HAS_SUPER_JUMP },
        { "Needle Strike", PlayerDataStrings.HAS_NEEDLE_STRIKE },
    };

        public static Dictionary<string, string> GameIdsToArchipelagoIds = ArchipelagoIdsToGameIds.ToDictionary(keyValuePair => keyValuePair.Value, keyValuePair => keyValuePair.Key);

        public static string GetInGameName(string archipelagoName)
        {
            return ArchipelagoIdsToGameIds.TryGetValue(archipelagoName, out var value) ? value : null;
        }

        public static string GetArchipelagoName(string inGameName)
        {
            return GameIdsToArchipelagoIds.TryGetValue(inGameName, out var value) ? value : null;
        }
    }
}
