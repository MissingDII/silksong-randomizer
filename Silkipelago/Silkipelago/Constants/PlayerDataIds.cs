using System.Collections.Generic;

namespace Silkipelago.Constants
{
    /// <summary>
    /// PlayerData string constants for tracking defeated enemies and bosses in Silksong.
    /// </summary>
    public static class PlayerDataIds
    {

        //stations
        public const string DOCK_STATION = "UnlockedDocksStation";
        public const string FAR_FIELDS_STATION = "UnlockedBoneforestEastStation";
        public const string GREYMOOR_STATION = "UnlockedGreymoorStation";
        public const string BELLHART_STATION = "UnlockedBelltownStation";
        public const string SHELLWOOD_STATION = "UnlockedShellwoodStation";
        public const string BLASTED_STEPS_STATION = "UnlockedCoralTowerStation";
        public const string SLAB_STATION = "UnlockedPeakStation";
        public const string GRAND_BELLWAY_STATION = "UnlockedCityStation";
        public const string BILEWATER_STATION = "UnlockedShadowStation";
        public const string AQUEDUCT_STATION = "UnlockedAqueductStation";
        public static readonly List<string> STATIONS =
        [
            DOCK_STATION,
            FAR_FIELDS_STATION,
            GREYMOOR_STATION,
            BELLHART_STATION,
            SHELLWOOD_STATION,
            BLASTED_STEPS_STATION,
            SLAB_STATION,
            GRAND_BELLWAY_STATION,
            BILEWATER_STATION,
            AQUEDUCT_STATION

        ];
        //tubes
        public const string MEMORIUM_TUBE = "UnlockedArboriumTube";
        public const string HIGH_HALLS_TUBE = "UnlockedHangTube";
        public const string SHRINE_TUBE = "UnlockedEnclaveTube";
        public const string CHAMBERS_TUBE = "UnlockedSongTube";
        public const string GRAND_BELLWAY_TUBE = "UnlockedCityBellwayTube";
        public const string UNDERWORK_TUBE = "UnlockedUnderTube";

        public static readonly List<string> TUBES =
        [
            MEMORIUM_TUBE,
            HIGH_HALLS_TUBE,
            SHRINE_TUBE,
            CHAMBERS_TUBE,
            GRAND_BELLWAY_TUBE,
            UNDERWORK_TUBE,
        ];

        public const string SILK_HEART = "silkRegenMax";
        //Eva upgrades
        public const string YELLOW_VESTICREST = "UnlockedExtraYellowSlot";
        public const string BLUE_VESTICREST = "UnlockedExtraBlueSlot";
        public const string SYLPHSONG = "HasBoundCrestUpgrader";

        public static readonly List<string> EVA_UPGRADES =
        [
            YELLOW_VESTICREST,
            BLUE_VESTICREST,
            SYLPHSONG
        ];
        //chapels
        public const string REAPER_CHAPEL = "chapelClosed_reaper";
        public const string WANDERER_CHAPEL = "chapelClosed_wanderer";
        public const string BEAST_CHAPEL = "chapelClosed_beast";
        public const string WITCH_CHAPEL = "chapelClosed_witch";
        public const string ARCHITECT_CHAPEL = "chapelClosed_toolmaster";
        public const string SHAMAN_CHAPEL = "chapelClosed_shaman";

        public static readonly List<string> CHAPELS =
        [
            REAPER_CHAPEL,
            WANDERER_CHAPEL,
            BEAST_CHAPEL,
            WITCH_CHAPEL,
            ARCHITECT_CHAPEL,
            SHAMAN_CHAPEL
        ];
        // Needolin and melodies
        public const string NEEDOLIN = "Needolin";
        public const string BEASTLING_CALL = "UnlockedFastTravelTeleport";
        public const string ELEGY_OF_THE_DEEP = "hasNeedolinMemoryPowerup";
        public const string CONDUCTOR_MELODY = "HasMelodyConductor";
        public const string ARCHITECT_MELODY = "HasMelodyArchitect";
        public const string VAULTKEEPER_MELODY = "HasMelodyLibrarian";

        public static readonly List<string> MELODIES =
        [
            NEEDOLIN,
            BEASTLING_CALL,
            ELEGY_OF_THE_DEEP,
            CONDUCTOR_MELODY,
            ARCHITECT_MELODY,
            VAULTKEEPER_MELODY
        ];
        //silk abilities
        public const string SILK_SPEAR = "hasNeedleThrow";
        public const string THREAD_STORM = "hasThreadSphere";
        public const string CROSS_STITCH = "hasParry";
        public const string SHARP_DART = "hasSilkCharge";
        public const string RUNE_RAGE = "hasSilkBomb";
        public const string PALE_NAILS = "hasSilkBomb";
        public const string SILK_SPECIAL = "hasSilkSpecial";

        public static readonly List<string> SILK_ABILITIES =
        [
            SILK_SPEAR,
            THREAD_STORM,
            CROSS_STITCH,
            SHARP_DART,
            PALE_NAILS
        ];
        //cutscene
        public const string BIND_CUTSCENE = "bindCutscenePlayed";

        public static readonly List<string> CUTSCENES =
        [
            BIND_CUTSCENE
        ];

        //shrine
        public const string SHRINE_BONE = "bellShrineBoneForest";
        public const string SHRINE_WILDS = "bellShrineWilds";
        public const string SHRINE_GREYMOOR = "bellShrineGreymoor";
        public const string SHRINE_SHELLWOOD = "bellShrineShellwood";
        public const string SHRINE_BELLHART = "bellShrineBellhart";
        public const string SHRINE_ENCLAVE = "bellShrineEnclave";

        public static readonly List<string> SHRINES =
        [
            SHRINE_BONE,
            SHRINE_WILDS,
            SHRINE_GREYMOOR,
            SHRINE_SHELLWOOD,
            SHRINE_BELLHART,
            SHRINE_ENCLAVE,
        ];

        // Keys
        public const string INDOLENT_KEY = "HasSlabKeyA";
        public const string HERETIC_KEY = "HasSlabKeyB";
        public const string APOSTATE_KEY = "HasSlabKeyC";

        public static readonly List<string> KEYS =
        [
            INDOLENT_KEY,
            HERETIC_KEY,
            APOSTATE_KEY,
        ];
        // Abilities
        public const string HAS_NEEDOLIN = "hasNeedolin";
        public const string HAS_DASH = "hasDash";
        public const string HAS_DRIFTER_CLOAK = "hasBrolly";
        public const string HAS_WALL_JUMP = "hasWalljump";
        public const string HAS_DOUBLE_JUMP = "hasDoubleJump";
        public const string HAS_SUPER_JUMP = "hasSuperJump";
        public const string HAS_HARPOON_DASH = "hasHarpoonDash";
        public const string HAS_NEEDLE_STRIKE = "hasChargeSlash";


        public static readonly List<string> ABILITIES =
        [
            HAS_NEEDOLIN,
            HAS_DASH,
            HAS_DRIFTER_CLOAK,
            HAS_WALL_JUMP,
            HAS_DOUBLE_JUMP,
            HAS_SUPER_JUMP,
            HAS_HARPOON_DASH,
            HAS_NEEDLE_STRIKE
        ];

        // Pilgrims
        public const string DICE_PILGRIM_DEFEATED = "dicePilgrimDefeated";
        public const string BONEGRAVE_ROSARY_PILGRIM_DEFEATED = "bonegraveRosaryPilgrimDefeated";
        public const string SHELLWOOD_ROSARY_PILGRIM_DEFEATED = "defeatedShellwoodRosaryPilgrim";

        // Major Bosses
        public const string BELL_BEAST_DEFEATED = "UnlockedFastTravel";
        public const string GARMOND_LIBRARY_HORNET_DEFEATED = "garmondLibraryDefeatedHornet";
        public const string GARMOND_BLACK_THREAD_DEFEATED = "garmondBlackThreadDefeated";
        public const string MOSS_MOTHER_DEFEATED = "defeatedMossMother";
        public const string MOSS_EVOLVER_DEFEATED = "defeatedMossEvolver";
        public const string SKULL_KING_DEFEATED = "skullKingDefeated";
        public const string SKULL_KING_BLACK_THREADED_DEFEATED = "skullKingDefeatedBlackThreaded";
        public const string SONG_GOLEM_DEFEATED = "defeatedSongGolem";
        public const string LACE_DEFEATED = "defeatedLace1";
        public const string CROW_COURT_DEFEATED = "defeatedCrowCourt";
        public const string WISP_PYRE_EFFIGY_DEFEATED = "defeatedWispPyreEffigy";
        public const string SPINNER_DEFEATED = "spinnerDefeated";
        public const string SPINNER_DEFEATED_AFTER_TIME = "SpinnerDefeatedTimePassed";
        public const string SPLINTER_QUEEN_DEFEATED = "defeatedSplinterQueen";
        public const string SETH_DEFEATED = "defeatedSeth";
        public const string FLOWER_QUEEN_DEFEATED = "defeatedFlowerQueen";
        public const string ROACHKEEPER_CHEF_DEFEATED = "defeatedRoachkeeperChef";
        public const string PHANTOM_DEFEATED = "defeatedPhantom";
        public const string SWAMP_SHAMAN_DEFEATED = "DefeatedSwampShaman";
        public const string CORAL_KING_DEFEATED = "defeatedCoralKing";
        public const string LAST_JUDGE_DEFEATED = "defeatedLastJudge";
        public const string GREY_WARRIOR_DEFEATED = "defeatedGreyWarrior";
        public const string FIRST_WEAVER_DEFEATED = "defeatedFirstWeaver";
        public const string BROOD_MOTHER_DEFEATED = "defeatedBroodMother";
        public const string SONG_CHEVALIER_BOSS_DEFEATED = "defeatedSongChevalierBoss";
        public const string WHITE_CLOVER_STAG_DEFEATED = "defeatedWhiteCloverstag";
        public const string LACE_TOWER_DEFEATED = "defeatedLaceTower";
        public const string WARD_BOSS_DEFEATED = "wardBossDefeated";
        public const string COG_7_AUTOMATON_DEFEATED = "cog7_automaton_defeated";
        public const string TROBBIO_DEFEATED = "defeatedTrobbio";
        public const string TORMENTED_TROBBIO_DEFEATED = "defeatedTormentedTrobbio";
        public const string COGWORK_DANCERS_DEFEATED = "defeatedCogworkDancers";
        public const string CLOVER_DANCERS_DEFEATED = "defeatedCloverDancers";

        // Zone Bosses
        public const string BONE_TOWN_BOSS_DEFEATED = "DefeatedBonetownBoss";
        public const string ROOF_CRAB_DEFEATED = "roofCrabDefeated";
        public const string DOCK_FOREMAN_DEFEATED = "defeatedDockForemen";

        // Bone Zone Enemies
        public const string BONE_FLYER_GIANT_DEFEATED = "defeatedBoneFlyerGiant";
        public const string BONE_FLYER_GIANT_GOLEM_SCENE_DEFEATED = "defeatedBoneFlyerGiantGolemScene";
        public const string ROCK_ROLLER_BONE_01_DEFEATED = "rockRollerDefeated_bone01";
        public const string ROCK_ROLLER_BONE_06_DEFEATED = "rockRollerDefeated_bone06";
        public const string ROCK_ROLLER_BONE_07_DEFEATED = "rockRollerDefeated_bone07";

        // Ant Zone Enemies
        public const string ANT_GUARD_02_DEFEATED = "ant02GuardDefeated";
        public const string ANT_QUEEN_DEFEATED = "defeatedAntQueen";
        public const string ANT_QUEEN_AFTER_RED_MEMORY_DEFEATED = "defeatedAntQueenAfterRedMemory";
        public const string ANT_TRAPPER_DEFEATED = "defeatedAntTrappers";

        // Coral Zone Enemies
        public const string CORAL_DRILLERS_DEFEATED = "defeatedCoralDrillers";
        public const string CORAL_BRIDGE_GUARD_1_DEFEATED = "defeatedCoralBridgeGuard1";
        public const string CORAL_BRIDGE_GUARD_2_DEFEATED = "defeatedCoralBridgeGuard2";
        public const string CORAL_DRILLER_SOLO_DEFEATED = "defeatedCoralDrillerSolo";
        public const string ZAP_CORE_ENEMY_DEFEATED = "defeatedZapCoreEnemy";
        public const string ZAP_GUARD_1_DEFEATED = "defeatedZapGuard1";
        public const string VAMPIRE_GNAT_BOSS_DEFEATED = "defeatedVampireGnatBoss";
        public const string VAMPIRE_GNAT_DEFEATED_BEFORE_CARAVAN_ARRIVED = "VampireGnatDefeatedBeforeCaravanArrived";

        // Guard/Keeper Enemies
        public const string GUARD_BONE_EAST_25_DEFEATED = "defeatedGuardBoneEast25";
        public const string SHELLWOOD_SLABFLY_DEFEATED = "shellwoodSlabflyDefeated";

        public static readonly List<string> BOSSES =
        [
            DICE_PILGRIM_DEFEATED,
            BONEGRAVE_ROSARY_PILGRIM_DEFEATED,
            SHELLWOOD_ROSARY_PILGRIM_DEFEATED,
            GARMOND_LIBRARY_HORNET_DEFEATED,
            GARMOND_BLACK_THREAD_DEFEATED,
            MOSS_MOTHER_DEFEATED,
            MOSS_EVOLVER_DEFEATED,
            SKULL_KING_DEFEATED,
            SKULL_KING_BLACK_THREADED_DEFEATED,
            BELL_BEAST_DEFEATED,
            SONG_GOLEM_DEFEATED,
            LACE_DEFEATED,
            CROW_COURT_DEFEATED,
            WISP_PYRE_EFFIGY_DEFEATED,
            SPINNER_DEFEATED,
            SPINNER_DEFEATED_AFTER_TIME,
            SPLINTER_QUEEN_DEFEATED,
            SETH_DEFEATED,
            FLOWER_QUEEN_DEFEATED,
            ROACHKEEPER_CHEF_DEFEATED,
            PHANTOM_DEFEATED,
            SWAMP_SHAMAN_DEFEATED,
            CORAL_KING_DEFEATED,
            LAST_JUDGE_DEFEATED,
            GREY_WARRIOR_DEFEATED,
            FIRST_WEAVER_DEFEATED,
            BROOD_MOTHER_DEFEATED,
            SONG_CHEVALIER_BOSS_DEFEATED,
            WHITE_CLOVER_STAG_DEFEATED,
            LACE_TOWER_DEFEATED,
            WARD_BOSS_DEFEATED,
            COG_7_AUTOMATON_DEFEATED,
            BONE_TOWN_BOSS_DEFEATED,
            ROOF_CRAB_DEFEATED,
            DOCK_FOREMAN_DEFEATED,
            BONE_FLYER_GIANT_DEFEATED,
            BONE_FLYER_GIANT_GOLEM_SCENE_DEFEATED,
            ROCK_ROLLER_BONE_01_DEFEATED,
            ROCK_ROLLER_BONE_06_DEFEATED,
            ROCK_ROLLER_BONE_07_DEFEATED,
            ANT_GUARD_02_DEFEATED,
            ANT_QUEEN_DEFEATED,
            ANT_QUEEN_AFTER_RED_MEMORY_DEFEATED,
            ANT_TRAPPER_DEFEATED,
            CORAL_DRILLERS_DEFEATED,
            CORAL_BRIDGE_GUARD_1_DEFEATED,
            CORAL_BRIDGE_GUARD_2_DEFEATED,
            CORAL_DRILLER_SOLO_DEFEATED,
            ZAP_CORE_ENEMY_DEFEATED,
            ZAP_GUARD_1_DEFEATED,
            VAMPIRE_GNAT_BOSS_DEFEATED,
            VAMPIRE_GNAT_DEFEATED_BEFORE_CARAVAN_ARRIVED,
            GUARD_BONE_EAST_25_DEFEATED,
            SHELLWOOD_SLABFLY_DEFEATED,
            TROBBIO_DEFEATED,
            TORMENTED_TROBBIO_DEFEATED,
            COGWORK_DANCERS_DEFEATED,
            CLOVER_DANCERS_DEFEATED
        ];

        // Fleas
        public const string FLEA_BONE_06 = "SavedFlea_Bone_06";
        public const string FLEA_DOCK_16 = "SavedFlea_Dock_16";
        public const string FLEA_BONE_EAST_05 = "SavedFlea_Bone_East_05";
        public const string FLEA_BONE_EAST_17B = "SavedFlea_Bone_East_17b";
        public const string FLEA_ANT_03 = "SavedFlea_Ant_03";
        public const string FLEA_GREYMOOR_15B = "SavedFlea_Greymoor_15b";
        public const string FLEA_GREYMOOR_06 = "SavedFlea_Greymoor_06";
        public const string FLEA_SHELLWOOD_03 = "SavedFlea_Shellwood_03";
        public const string FLEA_BONE_EAST_10_CHURCH = "SavedFlea_Bone_East_10_Church";
        public const string FLEA_CORAL_35 = "SavedFlea_Coral_35";
        public const string FLEA_DUST_12 = "SavedFlea_Dust_12";
        public const string FLEA_DUST_09 = "SavedFlea_Dust_09";
        public const string FLEA_BELLTOWN_04 = "SavedFlea_Belltown_04";
        public const string FLEA_CRAWL_06 = "SavedFlea_Crawl_06";
        public const string FLEA_SLAB_CELL = "SavedFlea_Slab_Cell";
        public const string FLEA_SHADOW_28 = "SavedFlea_Shadow_28";
        public const string FLEA_DOCK_03D = "SavedFlea_Dock_03d";
        public const string FLEA_UNDER_23 = "SavedFlea_Under_23";
        public const string FLEA_SHADOW_10 = "SavedFlea_Shadow_10";
        public const string FLEA_SONG_14 = "SavedFlea_Song_14";
        public const string FLEA_CORAL_24 = "SavedFlea_Coral_24";
        public const string FLEA_PEAK_05C = "SavedFlea_Peak_05c";
        public const string FLEA_LIBRARY_09 = "SavedFlea_Library_09";
        public const string FLEA_SONG_11 = "SavedFlea_Song_11";
        public const string FLEA_LIBRARY_01 = "SavedFlea_Library_01";
        public const string FLEA_UNDER_21 = "SavedFlea_Under_21";
        public const string FLEA_SLAB_06 = "SavedFlea_Slab_06";
        public const string FLEA_MEMORIUM_GIANT = "SavedFlea_Memorium_Giant";
        public const string FLEA_VOG_PUTRIFIED = "SavedFlea_Vog_Putrified";
        public const string KRAT_SAVED = "CaravanLechSaved";

        public static readonly List<string> FLEAS =
        [
            FLEA_BONE_06,
            FLEA_DOCK_16,
            FLEA_BONE_EAST_05,
            FLEA_BONE_EAST_17B,
            FLEA_ANT_03,
            FLEA_GREYMOOR_15B,
            FLEA_GREYMOOR_06,
            FLEA_SHELLWOOD_03,
            FLEA_BONE_EAST_10_CHURCH,
            FLEA_CORAL_35,
            FLEA_DUST_12,
            FLEA_DUST_09,
            FLEA_BELLTOWN_04,
            FLEA_CRAWL_06,
            FLEA_SLAB_CELL,
            FLEA_SHADOW_28,
            FLEA_DOCK_03D,
            FLEA_UNDER_23,
            FLEA_SHADOW_10,
            FLEA_SONG_14,
            FLEA_CORAL_24,
            FLEA_PEAK_05C,
            FLEA_LIBRARY_09,
            FLEA_SONG_11,
            FLEA_LIBRARY_01,
            FLEA_UNDER_21,
            FLEA_SLAB_06,
            FLEA_MEMORIUM_GIANT,
            FLEA_VOG_PUTRIFIED,
            KRAT_SAVED
        ];
    }

}
