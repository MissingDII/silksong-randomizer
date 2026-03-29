using System.Collections.Generic;
using System.Linq;

namespace Silkipelago.Constants
{
    internal class ArchipelagoLocationIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {
         //Objectives
        { "Save: The Threadspun Town", QuestIds.THREADSPUN_TOWN },
        { "Seek: The Great Citadel", QuestIds.CITADEL_SEEKER },
        { "Search: Silent Halls", QuestIds.CITADEL_INVESTIGATE },
        { "Ascend: Pharloom's Crown", QuestIds.CITADEL_ASCENT },
        { "Seek: After The Fall", QuestIds.BLACK_THREAD_PT0 },
        { "Seek: Awaiting The End", QuestIds.BLACK_THREAD_PT1_SHAMANS },
        { "Seek: The Dark Below", QuestIds.BLACK_THREAD_PT2_ABYSS },
        { "Ascend: Return To Pharloom", QuestIds.BLACK_THREAD_PT3_ESCAPE},
        { "Seek: Spell Seeker", QuestIds.BLACK_THREAD_PT4_RETURN },
        { "Seek: The Old Hearts", QuestIds.BLACK_THREAD_PT5_HEART },
        { "Snare Grand Mother Silk", SaveSlotCompletionIcons.CompletionState.Act2SoulSnare.ToString() },
        //Wishes
        { "Memory locket - Volatiles FlintBeetles", QuestIds.ROCK_ROLLERS },
        { "Wayfarer Wish: My Missing Courier", QuestIds.SAVE_COURIER_SHORT },
        { "Wayfarer Wish: My Missing Brother", QuestIds.SAVE_COURIER_TALL },
        { "Wayfarer Wish: Infestation Operation", QuestIds.DOCTOR_CURSE_CURE },
        { "Wayfarer Wish: Silk and Soul", QuestIds.SOUL_SNARE },
        { "Defeat Skull Tyrant (Wish)", QuestIds.SKULL_KING },
        //crest
        { "Bound the Crest of Reaper",CrestIds.REAPER },
        { "Bound the Crest of Wanderer",CrestIds.WANDERER },
        { "Bound the Crest of Beast", CrestIds.BEAST },
        { "Bound the Crest of Architect",CrestIds.ARCHITECT },
        { "Bound the Crest of Shaman",CrestIds.SHAMAN },
        // items
        { "Pickup Twisted Bud", CollectablesIds.TWISTED_BUD },
        { "Complete Red Memory", CollectablesIds.EVERBLOOM },
        // needolin and melodies 
        { "Learn: Conductor's Melody", PlayerDataIds.CONDUCTOR_MELODY },
        { "Learn: Architect's Melody", PlayerDataIds.ARCHITECT_MELODY },
        { "Learn: Vaultkeeper's Melody", PlayerDataIds.VAULTKEEPER_MELODY },
        //Tools skill
        { "Weaver Spire: Silkspear", PlayerDataIds.SILK_SPEAR },
        { "Weaver Spire: Thread Storm", PlayerDataIds.THREAD_STORM  },
        { "Weaver Spire: Sharpdart", PlayerDataIds.SHARP_DART },
        { "Acquire Pale Nails", PlayerDataIds.PALE_NAILS },
        //Cutscenes
        { "Bound the Needle", PlayerDataIds.BIND_CUTSCENE },
            //Shrines
        { "Ring The Bell In The Marrow", PlayerDataIds.SHRINE_BONE },
        { "Ring The Bell In Deep Docks", PlayerDataIds.SHRINE_WILDS },
        { "Ring The Bell In Shellwood", PlayerDataIds.SHRINE_SHELLWOOD },
        { "Ring The Bell In Greymoor", PlayerDataIds.SHRINE_GREYMOOR },
        { "Ring The Bell In Bellhart", PlayerDataIds.SHRINE_BELLHART },
        { "Ring The Bell In Songclave", PlayerDataIds.SHRINE_ENCLAVE },
        //keys
        { "Pickup Key of Indolent", PlayerDataIds.INDOLENT_KEY },
        { "Pickup Key of Heretic", PlayerDataIds.HERETIC_KEY },
        { "Pickup Key of Apostate", PlayerDataIds.APOSTATE_KEY },
        { "Twelfth Architect: Architect Key", CollectablesIds.ARCHITECT_KEY },
        { "Pickup White Key", CollectablesIds.WHITE_KEY },
        { "Pickup Surgeon's Key", CollectablesIds.WHITE_BOSS_KEY },
        //abilities
        { "Hunt Wish: Flexile Spines", PlayerDataIds.HAS_DRIFTER_CLOAK },
        { "Weaver Spire: Cling Grip", PlayerDataIds.HAS_WALL_JUMP },
        { "Weaver Spire: Swift Step", PlayerDataIds.HAS_DASH },
        { "Acquire Faydown Cloak", PlayerDataIds.HAS_DOUBLE_JUMP },
        { "Weaver Spire: Clawline", PlayerDataIds.HAS_HARPOON_DASH },
        { "Weaver Spire: Silk Soar", PlayerDataIds.HAS_SUPER_JUMP },
        { "Learn Needle Strike", PlayerDataIds.HAS_NEEDLE_STRIKE },

        //bosses
        { "Defeat Moss Mother", PlayerDataIds.MOSS_MOTHER_DEFEATED },
        { "Defeat Bell Beast",BossIds.BELL_BEAST },
        { "Defeat Bell Eater",PlayerDataIds.BEASTLING_CALL },
        { "Defeat Fourth Chorus", PlayerDataIds.SONG_GOLEM_DEFEATED },
        { "Defeat Moorwing", BossIds.MOORWING },
        { "Defeat Sister Splinter", PlayerDataIds.SPLINTER_QUEEN_DEFEATED },
        { "Defeat Widow", BossIds.WIDOW },
        { "Defeat Great Conchflies", PlayerDataIds.CORAL_DRILLERS_DEFEATED },
        { "Defeat Raging Conchfly", PlayerDataIds.CORAL_DRILLER_SOLO_DEFEATED },
        { "Defeat Last Judge", PlayerDataIds.LAST_JUDGE_DEFEATED },
        { "Defeat Cogwork Dancers", PlayerDataIds.COGWORK_DANCERS_DEFEATED },
        { "Defeat Clover Dancers", PlayerDataIds.CLOVER_DANCERS_DEFEATED },
        { "Defeat Trobbio", PlayerDataIds.TROBBIO_DEFEATED },
        { "Defeat Tormented Trobbio", PlayerDataIds.TORMENTED_TROBBIO_DEFEATED },
        { "Defeat Groal the Great", PlayerDataIds.SWAMP_SHAMAN_DEFEATED },
        { "Defeat The Unravelled", PlayerDataIds.WARD_BOSS_DEFEATED },
        { "Defeat Disgraced Chef Lugoli", PlayerDataIds.ROACHKEEPER_CHEF_DEFEATED },
        { "Defeat Craggler",BossIds.CRAGGLER },
        { "Defeat Father of the Flame", PlayerDataIds.WISP_PYRE_EFFIGY_DEFEATED },
        { "Defeat Voltvyrm", PlayerDataIds.ZAP_CORE_ENEMY_DEFEATED },
        { "Defeat Second Sentinel", PlayerDataIds.COG_7_AUTOMATON_DEFEATED },
        { "Defeat Broodmother", PlayerDataIds.BROOD_MOTHER_DEFEATED },
        { "Defeat Plasmified Zango", "10" }, // TODO validate
        { "Defeat Shrine Guardian Seth", PlayerDataIds.SETH_DEFEATED },
        { "Defeat Palestag", PlayerDataIds.WHITE_CLOVER_STAG_DEFEATED },
        { "Defeat Lost Garmond", PlayerDataIds.GARMOND_BLACK_THREAD_DEFEATED },
        { "Defeat Pinstress", "11" }, // TODO 
        { "Defeat Gurr the Outcast", PlayerDataIds.ANT_TRAPPER_DEFEATED },
        { "Defeat Watcher at the Edge", PlayerDataIds.GREY_WARRIOR_DEFEATED  },
        { "Defeat Crawfather", PlayerDataIds.CROW_COURT_DEFEATED },
        { "Defeat Shakra", "12" }, // TODO
        { "Defeat Garmond and Zaza", PlayerDataIds.GARMOND_LIBRARY_HORNET_DEFEATED },
        { "Defeat Savage Beastfly (Beast)", PlayerDataIds.BONE_FLYER_GIANT_DEFEATED },
        { "Defeat Savage Beastfly (Wish)", PlayerDataIds.BONE_FLYER_GIANT_GOLEM_SCENE_DEFEATED },
        { "Defeat Skull Tyrant (Bone Bottom)", PlayerDataIds.ROCK_ROLLER_BONE_01_DEFEATED },
        { "Defeat Phantom", PlayerDataIds.PHANTOM_DEFEATED },
        { "Defeat First Sinner", PlayerDataIds.FIRST_WEAVER_DEFEATED },
        { "Defeat Lace", BossIds.LACE},
        { "Defeat Lace (Cradle)", PlayerDataIds.LACE_TOWER_DEFEATED },
        { "Defeat Grand Mother Silk", SaveSlotCompletionIcons.CompletionState.Act2Regular.ToString() },
        { "Defeat Crust King Khann", PlayerDataIds.CORAL_KING_DEFEATED },
        { "Defeat Nyleth", PlayerDataIds.FLOWER_QUEEN_DEFEATED },
        { "Defeat Skarrsinger Karmelita", PlayerDataIds.ANT_QUEEN_DEFEATED }
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

