using System.Collections.Generic;
using System.Linq;


namespace Silkipelago.Constants
{
    public static class ArchipelagoItemIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {
         //Crest
         { "Progressive Hunter Crest", CrestIds.HUNTER },
         { "Reaper Crest", CrestIds.REAPER },
         { "Wanderer Crest", CrestIds.WANDERER },
         { "Beast Crest", CrestIds.BEAST },
         { "Architect Crest", CrestIds.ARCHITECT },
         { "Shaman Crest", CrestIds.SHAMAN },
         { "Witch Crest", CrestIds.WITCH },

         //Crest upgrade
         { "Yellow Vesticrest", PlayerDataIds.YELLOW_VESTICREST },
         { "Blue Vesticrest", PlayerDataIds.BLUE_VESTICREST },


        // items
         {"Silk Heart",PlayerDataIds.SILK_HEART },
         { "Twisted Bud", CollectablesIds.TWISTED_BUD },
         {"Soul Snare", QuestIds.SILK_DEFEAT_SNARE },
         { "Everbloom", CollectablesIds.EVERBLOOM },
         // needolin and melodies 
         { "Needolin", PlayerDataIds.HAS_NEEDOLIN },
         { "Beastling Call",PlayerDataIds.BEASTLING_CALL  },
         { "Elegy of the Deep", PlayerDataIds.ELEGY_OF_THE_DEEP },
         { "Conductor's Melody", PlayerDataIds.CONDUCTOR_MELODY },
         { "Architect's Melody", PlayerDataIds.ARCHITECT_MELODY },
         { "Vaultkeeper's Melody", PlayerDataIds.VAULTKEEPER_MELODY },
         //Tools
         { "Silkspear", ToolsIds.SILK_SPEAR },
         { "Thread Storm",ToolsIds.THREAD_STORM  },
         { "Sharpdart", ToolsIds.SHARP_DART },
         { "Pale Nails", ToolsIds.PALE_NAILS },
         { "Cross Stitch", ToolsIds.CROSS_STITCH },
         { "Rune Rage", ToolsIds.RUNE_RAGE },
         //Shrines
         { "Grand Gate - Marrow Bell", PlayerDataIds.SHRINE_BONE },
         { "Grand Gate - Deep Docks Bell", PlayerDataIds.SHRINE_WILDS },
         { "Grand Gate - Shellwood Bell", PlayerDataIds.SHRINE_SHELLWOOD },
         { "Grand Gate - Greymoor Bell", PlayerDataIds.SHRINE_GREYMOOR },
         { "Grand Gate - Bellhart Bell", PlayerDataIds.SHRINE_BELLHART },
         // TODO not added by alex yet
         { "Ring The Bell In Songclave", PlayerDataIds.SHRINE_ENCLAVE },
         //keys
         { "Key of Indolent", PlayerDataIds.INDOLENT_KEY },
         { "Key of Heretic", PlayerDataIds.HERETIC_KEY },
         { "Key of Apostate", PlayerDataIds.APOSTATE_KEY },
         { "Architect Key", CollectablesIds.ARCHITECT_KEY },
         { "White Key", CollectablesIds.WHITE_KEY },
         { "Surgeon's Key", CollectablesIds.WHITE_BOSS_KEY },
         // Crest Upgrades
         { "Memory Locket", CollectablesIds.MEMORY_LOCKET },
         { "Craft Metal", CollectablesIds.CRAFT_METAL },
         //abilities
         { "Sylphsong", PlayerDataIds.SYLPHSONG },
         { "Drifter's Cloak", PlayerDataIds.HAS_DRIFTER_CLOAK },
         { "Cling Grip", PlayerDataIds.HAS_WALL_JUMP },
         { "Swift Step", PlayerDataIds.HAS_DASH },
         { "Faydown Cloak", PlayerDataIds.HAS_DOUBLE_JUMP },
         { "Clawline", PlayerDataIds.HAS_HARPOON_DASH },
         { "Silk Soar", PlayerDataIds.HAS_SUPER_JUMP },
         { "Needle Strike", PlayerDataIds.HAS_NEEDLE_STRIKE },
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
