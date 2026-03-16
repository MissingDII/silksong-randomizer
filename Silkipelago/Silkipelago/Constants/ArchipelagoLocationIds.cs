using System.Collections.Generic;
using System.Linq;

namespace Silkipelago.Constants
{
    internal class ArchipelagoLocationIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {
        //crest
        { "Bound the Crest of Reaper",CrestStrings.REAPER },
        { "Bound the Crest of Wanderer",CrestStrings.WANDERER },
        { "Bound the Crest of Beast", CrestStrings.BEAST },
        { "Bound the Crest of Architect",CrestStrings.ARCHITECT },
        { "Bound the Crest of Shaman",CrestStrings.SHAMAN },
        // items
        { "Pickup Twisted Bud", CollectablesStrings.TWISTED_BUD },
        // needolin and melodies 
        { "Learn: Conductor's Melody", PlayerDataStrings.CONDUCTOR_MELODY },
        { "Learn: Architect's Melody", PlayerDataStrings.ARCHITECT_MELODY },
        { "Learn: Vaultkeeper's Melody", PlayerDataStrings.VAULTKEEPER_MELODY },
        //Tools skill
        { "Weaver Spire: Silkspear", ToolsStrings.SILK_SPEAR },
        { "Weaver Spire: Thread Storm",ToolsStrings.THREAD_STORM  },
        { "Weaver Spire: Sharpdart", ToolsStrings.SHARP_DART },
        { "Acquire Pale Nails", ToolsStrings.PALE_NAILS },
        //Cutscenes
        { "Bound the Needle", PlayerDataStrings.BIND_CUTSCENE },
            //Shrines
        { "Ring The Bell In The Marrow", PlayerDataStrings.SHRINE_BONE },
        { "Ring The Bell In Deep Docks", PlayerDataStrings.SHRINE_WILDS },
        { "Ring The Bell In Shellwood", PlayerDataStrings.SHRINE_SHELLWOOD },
        { "Ring The Bell In Greymoor", PlayerDataStrings.SHRINE_GREYMOOR },
        { "Ring The Bell In Bellhart", PlayerDataStrings.SHRINE_BELLHART },
        { "Ring The Bell In Songclave", PlayerDataStrings.SHRINE_ENCLAVE },
        //keys
        { "Pickup Key of Indolent", PlayerDataStrings.INDOLENT_KEY },
        { "Pickup Key of Heretic", PlayerDataStrings.HERETIC_KEY },
        { "Pickup Key of Apostate", PlayerDataStrings.APOSTATE_KEY },
        { "Twelfth Architect: Architect Key", CollectablesStrings.ARCHITECT_KEY },
        { "Pickup White Key", CollectablesStrings.WHITE_KEY },
        { "Pickup Surgeon's Key", CollectablesStrings.WHITE_BOSS_KEY },
        //abilities
        { "Hunt Wish: Flexile Spines", PlayerDataStrings.HAS_DRIFTER_CLOAK },
        { "Weaver Spire: Cling Grip", PlayerDataStrings.HAS_WALL_JUMP },
        { "Weaver Spire: Swift Step", PlayerDataStrings.HAS_DASH },
        { "Acquire Faydown Cloak", PlayerDataStrings.HAS_DOUBLE_JUMP },
        { "Weaver Spire: Clawline", PlayerDataStrings.HAS_HARPOON_DASH },
        { "Weaver Spire: Silk Soar", PlayerDataStrings.HAS_SUPER_JUMP },
        { "Learn Needle Strike", PlayerDataStrings.HAS_NEEDLE_STRIKE },

        //bosses
        { "Defeat Moss Mother", PlayerDataStrings.MOSS_MOTHER_DEFEATED },
        { "Defeat Bell Beast",PlayerDataStrings.BELL_BEAST_DEFEATED },
        { "Defeat Bell Eater",PlayerDataStrings.BEASTLING_CALL },
        { "Defeat Fourth Chorus", PlayerDataStrings.SONG_GOLEM_DEFEATED },
        { "Defeat Moorwing", PlayerDataStrings.VAMPIRE_GNAT_BOSS_DEFEATED },
        { "Defeat Sister Splinter", PlayerDataStrings.SPLINTER_QUEEN_DEFEATED },
        { "Defeat Widow", PlayerDataStrings.SPINNER_DEFEATED },
        { "Defeat Great Conchflies", PlayerDataStrings.CORAL_DRILLERS_DEFEATED },
        { "Defeat Raging Conchfly", PlayerDataStrings.CORAL_DRILLER_SOLO_DEFEATED },
        { "Defeat Last Judge", PlayerDataStrings.LAST_JUDGE_DEFEATED },
        { "Defeat Cogwork Dancers", PlayerDataStrings.COGWORK_DANCERS_DEFEATED },
        { "Defeat Clover Dancers", PlayerDataStrings.CLOVER_DANCERS_DEFEATED },
        { "Defeat Trobbio", PlayerDataStrings.TROBBIO_DEFEATED },
        { "Defeat Tormented Trobbio", PlayerDataStrings.TORMENTED_TROBBIO_DEFEATED },
        { "Defeat Groal the Great", PlayerDataStrings.SWAMP_SHAMAN_DEFEATED },
        { "Defeat The Unravelled", PlayerDataStrings.WARD_BOSS_DEFEATED },
        { "Defeat Disgraced Chef Lugoli", PlayerDataStrings.ROACHKEEPER_CHEF_DEFEATED },
        { "Defeat Craggler", PlayerDataStrings.ROOF_CRAB_DEFEATED },
        { "Defeat Father of the Flame", PlayerDataStrings.WISP_PYRE_EFFIGY_DEFEATED },
        { "Defeat Voltvyrm", PlayerDataStrings.ZAP_CORE_ENEMY_DEFEATED },
        { "Defeat Second Sentinel", PlayerDataStrings.COG_7_AUTOMATON_DEFEATED },
        { "Defeat Broodmother", PlayerDataStrings.BROOD_MOTHER_DEFEATED },
        { "Defeat Plasmified Zango", "1" }, // TODO validate
        { "Defeat Shrine Guardian Seth", PlayerDataStrings.SETH_DEFEATED },
        { "Defeat Palestag", PlayerDataStrings.WHITE_CLOVER_STAG_DEFEATED }, // TODO validate
        { "Defeat Lost Garmond", PlayerDataStrings.GARMOND_BLACK_THREAD_DEFEATED },
        { "Defeat Pinstress", "2" }, // TODO 
        { "Defeat Gurr the Outcast", PlayerDataStrings.ANT_TRAPPER_DEFEATED }, // TODO validate
        { "Defeat Watcher at the Edge", PlayerDataStrings.GREY_WARRIOR_DEFEATED  },
        { "Defeat Crawfather", PlayerDataStrings.CROW_COURT_DEFEATED },
        { "Defeat Shakra", "3" }, // TODO
        { "Defeat Garmond and Zaza", PlayerDataStrings.GARMOND_LIBRARY_HORNET_DEFEATED },
        { "Defeat Savage Beastfly (Beast)", PlayerDataStrings.BONE_FLYER_GIANT_DEFEATED },
        { "Defeat Savage Beastfly (Wish)", PlayerDataStrings.BONE_FLYER_GIANT_GOLEM_SCENE_DEFEATED },
        { "Defeat Skull Tyrant (Wish)", PlayerDataStrings.SKULL_KING_DEFEATED },
        { "Defeat Skull Tyrant (Bone Bottom)", PlayerDataStrings.ROCK_ROLLER_BONE_01_DEFEATED },
        { "Defeat Phantom", PlayerDataStrings.PHANTOM_DEFEATED },
        { "Defeat First Sinner", PlayerDataStrings.FIRST_WEAVER_DEFEATED },
        { "Defeat Lace", PlayerDataStrings.LACE_DEFEATED },
        { "Defeat Lace (Cradle)", PlayerDataStrings.LACE_TOWER_DEFEATED },
        { "Defeat Grand Mother Silk", SaveSlotCompletionIcons.CompletionState.Act2Regular.ToString() }, // TODO
        { "Defeat Crust King Khann", PlayerDataStrings.CORAL_KING_DEFEATED },
        { "Defeat Nyleth", PlayerDataStrings.FLOWER_QUEEN_DEFEATED },
        { "Defeat Skarrsinger Karmelita", PlayerDataStrings.ANT_QUEEN_DEFEATED }
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

