using Silkipelago.Constants.GameObject;
using System.Collections.Generic;
using System.Linq;

namespace Silkipelago.Constants
{
    internal class ArchipelagoLocationIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {

        // Tutorial Vine Cluster
        { "Tutorial: Breakable Walls 1", Tut_01.VINE_CLUSTER },
        { "Tutorial: Breakable Walls 2", Tut_01.VINE_CLUSTER_1 },
        { "Tutorial: Breakable Walls 3", Tut_01.VINE_CLUSTER_3 },
        //Tutorial Mobs
        { "Tutorial: Mossgrub", MonsterIds.MOSSGRUB },
        { "Tutorial: Mossmir", MonsterIds.MOSSMIR },
        //Stations
        { "Unlock Deep Docks Bellway", PlayerDataIds.DOCK_STATION },
        { "Unlock Far Fields Bellway", PlayerDataIds.FAR_FIELDS_STATION },
        { "Unlock Greymoor Bellway", PlayerDataIds.GREYMOOR_STATION },
        { "Unlock Bellhart Bellway", PlayerDataIds.BELLHART_STATION },
        { "Unlock Shellwood Bellway", PlayerDataIds.SHELLWOOD_STATION },
        { "Unlock Blasted Steps Bellway", PlayerDataIds.BLASTED_STEPS_STATION },
        { "Unlock The Slab Bellway", PlayerDataIds.SLAB_STATION },
        { "Unlock Choral Chambers Bellway", PlayerDataIds.GRAND_BELLWAY_STATION },
        { "Unlock Bilewater Bellway", PlayerDataIds.BILEWATER_STATION },
        { "Unlock Putrified Ducts Bellway", PlayerDataIds.AQUEDUCT_STATION },

        //Tubes
        { "Unlock Memorium Ventrica", PlayerDataIds.MEMORIUM_TUBE },
        { "Unlock High Halls Ventrica", PlayerDataIds.HIGH_HALLS_TUBE },
        { "Unlock First Shrine Ventrica", PlayerDataIds.SHRINE_TUBE },
        { "Unlock Choral Chambers Ventrica", PlayerDataIds.CHAMBERS_TUBE },
        { "Unlock Grand Bellway Ventrica", PlayerDataIds.GRAND_BELLWAY_TUBE },
        { "Unlock Underworks Ventrica", PlayerDataIds.UNDERWORK_TUBE },

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
        //Memory lockets
        { "Memory locket - Chapel of the Beast", CollectablesIds.MEMORY_LOCKET_CHAPEL_BEAST },
        { "Memory locket - Mort", CollectablesIds.MEMORY_LOCKET_MORT },
        { "Memory locket - Greymoor Bellway", CollectablesIds.MEMORY_LOCKET_GREYMOOR_BELLWAY },
        { "Memory locket - The Marrow", CollectablesIds.MEMORY_LOCKET_THE_MARROW },
        { "Memory locket - Wormways", CollectablesIds.MEMORY_LOCKET_WORMWAYS },
        { "Memory locket - Frey", CollectablesIds.MEMORY_LOCKET_FREY },
        { "Memory locket - Blasted Steps", CollectablesIds.MEMORY_LOCKET_BLASTED_STEPS },
        { "Memory locket - Bilewater", CollectablesIds.MEMORY_LOCKET_BILEWATER },
        { "Memory locket - Choral Chambers", CollectablesIds.MEMORY_LOCKET_CHORAL_CHAMBERS },
        { "Memory locket - Underworks", CollectablesIds.MEMORY_LOCKET_UNDERWORKS },
        { "Memory locket - Deep Docks", CollectablesIds.MEMORY_LOCKET_DEEP_DOCKS },
        { "Memory locket - Whispering Vaults", CollectablesIds.MEMORY_LOCKET_WHISPERING_VAULTS },
        { "Memory locket - Sands of Karak", CollectablesIds.MEMORY_LOCKET_SANDS_OF_KARAK },
        { "Memory locket - Halfway Home", CollectablesIds.MEMORY_LOCKET_HALFWAY_HOME },
        { "Memory locket - Memorium", CollectablesIds.MEMORY_LOCKET_MEMORIUM },
        { "Memory locket - The Slab", CollectablesIds.MEMORY_LOCKET_THE_SLAB },
        { "Memory locket - Bilewater Upper", CollectablesIds.MEMORY_LOCKET_BILEWATER_UPPER },
        { "Memory locket - Bellhart Ceiling", CollectablesIds.MEMORY_LOCKET_BELLHART_CEILING },
        { "Memory locket - Far Fields East", CollectablesIds.MEMORY_LOCKET_FAR_FIELDS_EAST },
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
            //Tools
        { "Wayfarer Wish: The Lost Fleas",ToolsIds.FLEA_BREW },
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
        { "Defeat Bell Eater",PlayerDataIds.BEASTLING_CALL },//todo
        { "Defeat Fourth Chorus", BossIds.FOURTH_CHORUS },
        { "Defeat Moorwing", BossIds.MOORWING },
        { "Defeat Sister Splinter", BossIds.SPLINTER_QUEEN },
        { "Defeat Widow", BossIds.WIDOW },
        { "Defeat Great Conchflies", BossIds.GREAT_CONCHFLY },
        { "Defeat Raging Conchfly", BossIds.RAGING_CONCHFLY },
        { "Defeat Last Judge", PlayerDataIds.LAST_JUDGE_DEFEATED }, // to migrate to bossIds
        { "Defeat Cogwork Dancers", BossIds.COG_DANCERS },
        { "Defeat Clover Dancers", PlayerDataIds.CLOVER_DANCERS_DEFEATED }, //todo
        { "Defeat Trobbio", BossIds.TROBBIO },
        { "Defeat Tormented Trobbio", PlayerDataIds.TORMENTED_TROBBIO_DEFEATED },
        { "Defeat Groal the Great", BossIds.GROAL },
        { "Defeat The Unravelled", BossIds.UNRAVELLED },
        { "Defeat Disgraced Chef Lugoli", BossIds.CHEF_LUGOLI },
        { "Defeat Craggler",BossIds.CRAGGLER },
        { "Defeat Father of the Flame", PlayerDataIds.WISP_PYRE_EFFIGY_DEFEATED }, //todo
        { "Defeat Voltvyrm", BossIds.VOLTWYRM },
        { "Defeat Second Sentinel", BossIds.SECOND_SENTINEL },
        { "Defeat Broodmother", BossIds.BROODMOTHER },
        { "Defeat Plasmified Zango", "10" }, // TODO validate
        { "Defeat Shrine Guardian Seth", PlayerDataIds.SETH_DEFEATED },
        { "Defeat Palestag", PlayerDataIds.WHITE_CLOVER_STAG_DEFEATED },
        { "Defeat Lost Garmond", PlayerDataIds.GARMOND_BLACK_THREAD_DEFEATED },
        { "Defeat Pinstress", "11" }, // TODO 
        { "Defeat Gurr the Outcast", PlayerDataIds.ANT_TRAPPER_DEFEATED },
        { "Defeat Watcher at the Edge", PlayerDataIds.GREY_WARRIOR_DEFEATED  },
        { "Defeat Crawfather", PlayerDataIds.CROW_COURT_DEFEATED },
        { "Defeat Shakra", "12" }, // TODO
        { "Defeat Garmond and Zaza", BossIds.GARMOND },
        { "Defeat Savage Beastfly (Beast)", BossIds.SAVAGE_BEASTFLY },
        { "Defeat Savage Beastfly (Wish)", BossIds.SAVAGE_BEASTFLY_WISH },
        { "Defeat Skull Tyrant (Bone Bottom)", PlayerDataIds.ROCK_ROLLER_BONE_01_DEFEATED },
        { "Defeat Phantom",BossIds.PHANTOM },
        { "Defeat First Sinner", BossIds.FIRST_SINNER },
        { "Defeat Lace", BossIds.LACE},
        { "Defeat Lace (Cradle)", BossIds.LACE_CRADLE },
        { "Defeat Grand Mother Silk", SaveSlotCompletionIcons.CompletionState.Act2Regular.ToString() },
        { "Defeat Crust King Khann", PlayerDataIds.CORAL_KING_DEFEATED },
        { "Defeat Nyleth", PlayerDataIds.FLOWER_QUEEN_DEFEATED },
        { "Defeat Skarrsinger Karmelita", PlayerDataIds.ANT_QUEEN_DEFEATED },

        // Lost Fleas
        { "Lost Flea - Wormways - Carried by Aknid", PlayerDataIds.FLEA_CRAWL_06 },
        { "Lost Flea - The Marrow - Stuck In Vines", PlayerDataIds.FLEA_BONE_06 },
        { "Lost Flea - Hunter's March - Cage", PlayerDataIds.FLEA_ANT_03 },
        { "Lost Flea - Deep Docks - Lava Falls Room", PlayerDataIds.FLEA_DOCK_16 },
        { "Lost Flea - Deep Docks - Stuck Above Swift Step", PlayerDataIds.FLEA_BONE_EAST_05 },
        { "Lost Flea - Deep Docks - Underground After Arena", PlayerDataIds.FLEA_DOCK_03D },
        { "Lost Flea - Far Fields - Pressure Plate Cage", PlayerDataIds.FLEA_BONE_EAST_17B },
        { "Lost Flea - Far Fields - Pilgrim's Rest Behind Rhinogrund", PlayerDataIds.FLEA_BONE_EAST_10_CHURCH },
        { "Lost Flea - Greymoor - Stuck Above Craw Lake", PlayerDataIds.FLEA_GREYMOOR_15B },
        { "Lost Kratt - Greymoor", PlayerDataIds.KRAT_SAVED },
        { "Lost Flea - Greymoor - Top Of Left Tower", PlayerDataIds.FLEA_GREYMOOR_06 },
        { "Lost Flea - Bellhart - Stuck In Bells", PlayerDataIds.FLEA_BELLTOWN_04 },
        { "Lost Flea - Shellwood - Stuck In Vines", PlayerDataIds.FLEA_SHELLWOOD_03 },
        { "Lost Flea - Blasted Steps - Top Of Shaft", PlayerDataIds.FLEA_CORAL_35 },
        { "Lost Flea - Sinner's Road - Cage", PlayerDataIds.FLEA_DUST_12 },
        { "Lost Flea - Bilewater - Hiding From Snitchflies", PlayerDataIds.FLEA_SHADOW_28 },
        { "Lost Flea - Exhaust Organ - Stuck In Vines", PlayerDataIds.FLEA_DUST_09 },
        { "Lost Flea - Bilewater - Stuck In Vines", PlayerDataIds.FLEA_SHADOW_10 },
        { "Lost Flea - The Slab - Round Cage", PlayerDataIds.FLEA_SLAB_CELL },
        { "Lost Flea - The Slab - Hiding Above Bench", PlayerDataIds.FLEA_SLAB_06 },
        { "Lost Flea - Mount Fay - Frozen", PlayerDataIds.FLEA_PEAK_05C },
        { "Lost Flea - Choral Chambers - Fancy Cage", PlayerDataIds.FLEA_SONG_14 },
        { "Lost Flea - Choral Chambers - Vertical Vent", PlayerDataIds.FLEA_SONG_11 },
        { "Lost Flea - Whispering Vaults - Fancy Cage", PlayerDataIds.FLEA_LIBRARY_01 },
        { "Lost Flea - Songclave - Alcove Outside", PlayerDataIds.FLEA_LIBRARY_09 },
        { "Lost Flea - Underworks - Stuck Under Spool Fragment", PlayerDataIds.FLEA_UNDER_21 },
        { "Lost Flea - Underworks - Stuck After Saw Room", PlayerDataIds.FLEA_UNDER_23 },
        { "Giant Lost Flea - Memorium", PlayerDataIds.FLEA_MEMORIUM_GIANT },
        { "Lost Vog - Putrified Ducts", PlayerDataIds.FLEA_VOG_PUTRIFIED },
        { "Lost Flea - Sands Of Karak - Stuck In Spikes", PlayerDataIds.FLEA_CORAL_24 }
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

