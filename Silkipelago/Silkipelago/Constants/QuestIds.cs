using System.Collections.Generic;

namespace Silkipelago.Constants
{
    /// <summary>
    /// Quest ID string constants for tracking quest progression in Silksong.
    /// </summary>
    public static class QuestIds
    {
        // Main Quest Line - Citadel
        public const string CITADEL_SEEKER = "Citadel Seeker";
        public const string THREADSPUN_TOWN = "The Threadspun Town";
        public const string GRAND_GATE_BELLSHRINES = "Grand Gate Bellshrines";
        public const string CITADEL_INVESTIGATE = "Citadel Investigate";
        public const string CITADEL_ASCENT = "Citadel Ascent";
        public const string CITADEL_ASCENT_MELODIES = "Citadel Ascent Melodies";
        public const string CITADEL_ASCENT_LIFT = "Citadel Ascent Lift";
        public const string CITADEL_ASCENT_SILK_DEFEAT = "Citadel Ascent Silk Defeat";

        // Main Quest Line - Black Thread
        public const string SILK_DEFEAT_SNARE = "Silk Defeat Snare";
        public const string BLACK_THREAD_PT0 = "Black Thread Pt0";
        public const string BLACK_THREAD_PT1_SHAMANS = "Black Thread Pt1 Shamans";
        public const string BLACK_THREAD_PT2_ABYSS = "Black Thread Pt2 Abyss";
        public const string BLACK_THREAD_PT3_ESCAPE = "Black Thread Pt3 Escape";
        public const string BLACK_THREAD_PT4_RETURN = "Black Thread Pt4 Return";
        public const string BLACK_THREAD_PT5_HEART = "Black Thread Pt5 Heart";
        public const string BLACK_THREAD_PT6_FLOWER = "Black Thread Pt6 Flower";

        // Main Quest Line - Diving Bell
        public const string BELLBEAST_RESCUE = "Bellbeast Rescue";
        public const string DIVING_BELL_PT1_INSPECT = "Diving Bell Pt1 Inspect";
        public const string DIVING_BELL_PT2_BALLOW = "Diving Bell Pt2 Ballow";
        public const string DIVING_BELL_PT3_DESCEND = "Diving Bell Pt3 Descend";

        // Building Quests
        public const string BUILDING_MATERIALS = "Building Materials";
        public const string BUILDING_MATERIALS_BRIDGE = "Building Materials (Bridge)";
        public const string BUILDING_MATERIALS_STATUE = "Building Materials (Statue)";

        // Collection Quests
        public const string PILGRIM_RAGS = "Pilgrim Rags";
        public const string MOSSBERRY_COLLECTION_PRE = "Mossberry Collection Pre";
        public const string MOSSBERRY_COLLECTION_1 = "Mossberry Collection 1";
        public const string CROW_FEATHERS_PRE = "Crow Feathers Pre";
        public const string CROW_FEATHERS = "Crow Feathers";
        public const string BEASTFLY_HUNT = "Beastfly Hunt";
        public const string SHELL_FLOWERS = "Shell Flowers";

        // Save/NPC Quests
        public const string SAVE_THE_FLEAS_PRE = "Save the Fleas Pre";
        public const string SAVE_THE_FLEAS = "Save the Fleas";
        public const string SAVE_COURIER_SHORT = "Save Courier Short";
        public const string SAVE_COURIER_TALL = "Save Courier Tall";
        public const string SAVE_CITY_MERCHANT = "Save City Merchant";
        public const string SAVE_CITY_MERCHANT_BRIDGE = "Save City Merchant Bridge";
        public const string SAVE_SHERMA = "Save Sherma";

        // Courier Delivery Quests
        public const string COURIER_DELIVERY_BONEBOTTOM = "Courier Delivery Bonebottom";
        public const string COURIER_DELIVERY_PILGRIMS_REST = "Courier Delivery Pilgrims Rest";
        public const string COURIER_DELIVERY_SONGCLAVE = "Courier Delivery Songclave";
        public const string COURIER_DELIVERY_FLEATOPIA = "Courier Delivery Fleatopia";
        public const string COURIER_DELIVERY_MASK_MAKER = "Courier Delivery Mask Maker";
        public const string COURIER_DELIVERY_DUSTPENS_SLAVE = "Courier Delivery Dustpens Slave";
        public const string COURIER_DELIVERY_FIXER = "Courier Delivery Fixer";

        // Character Quests
        public const string BELLTOWN_HOUSE_START = "Belltown House Start";
        public const string BELLTOWN_HOUSE_MID = "Belltown House Mid";
        public const string PINSMITHS_TOOLS = "A Pinsmiths Tools";
        public const string SHINY_BELL_GOOMBA = "Shiny Bell Goomba";
        public const string WOOD_WITCH_CURSE = "Wood Witch Curse";
        public const string DOCTOR_CURSE_CURE = "Doctor Curse Cure";
        public const string SONG_PILGRIM_CLOAKS = "Song Pilgrim Cloaks";
        public const string FINE_PINS = "Fine Pins";
        public const string SONG_KNIGHT = "Song Knight";
        public const string HUNTRESS_QUEST = "Huntress Quest";
        public const string HUNTRESS_QUEST_RUNT = "Huntress Quest Runt";
        public const string SHAKRA_FINAL_QUEST = "Shakra Final Quest";

        // Combat/Boss Quests
        public const string ROCK_ROLLERS = "Rock Rollers";
        public const string SKULL_KING = "Skull King";
        public const string ROACH_KILLING = "Roach Killing";
        public const string BROODMOTHER_HUNT = "Broodmother Hunt";
        public const string GREAT_GOURMAND = "Great Gourmand";
        public const string PINSTRESS_BATTLE_PRE = "Pinstress Battle Pre";
        public const string PINSTRESS_BATTLE = "Pinstress Battle";
        public const string SPRINTMASTER_RACE = "Sprintmaster Race";
        public const string GARMOND_BLACK_THREADED = "Garmond Black Threaded";
        public const string TORMENTED_TROBBIO = "Tormented Trobbio";
        public const string ANT_TRAPPER = "Ant Trapper";
        public const string STEEL_SENTINEL = "Steel Sentinel";
        public const string STEEL_SENTINEL_PT2 = "Steel Sentinel Pt2";

        // Miscellaneous Quests
        public const string BROLLY_GET = "Brolly Get";
        public const string JOURNAL = "Journal";
        public const string EXTRACTOR_BLUE = "Extractor Blue";
        public const string EXTRACTOR_BLUE_WORMS = "Extractor Blue Worms";
        public const string SONGCLAVE_DONATION_1 = "Songclave Donation 1";
        public const string SONGCLAVE_DONATION_2 = "Songclave Donation 2";
        public const string FLEA_GAMES_PRE = "Flea Games Pre";
        public const string FLEA_GAMES = "Flea Games";
        public const string MR_MUSHROOM = "Mr Mushroom";
        public const string DESTROY_THREAD_CORES = "Destroy Thread Cores";
        public const string SPRINTMASTER_PRE = "Sprintmaster Pre";
        public const string SOUL_SNARE_PRE = "Soul Snare Pre";
        public const string SOUL_SNARE = "Soul Snare";

        public static readonly List<string> ALL_QUESTS = new()
        {
            CITADEL_SEEKER,
            THREADSPUN_TOWN,
            GRAND_GATE_BELLSHRINES,
            CITADEL_INVESTIGATE,
            CITADEL_ASCENT,
            CITADEL_ASCENT_MELODIES,
            CITADEL_ASCENT_LIFT,
            CITADEL_ASCENT_SILK_DEFEAT,
            SILK_DEFEAT_SNARE,
            BLACK_THREAD_PT0,
            BLACK_THREAD_PT1_SHAMANS,
            BLACK_THREAD_PT2_ABYSS,
            BLACK_THREAD_PT3_ESCAPE,
            BLACK_THREAD_PT4_RETURN,
            BLACK_THREAD_PT5_HEART,
            BLACK_THREAD_PT6_FLOWER,
            BELLBEAST_RESCUE,
            DIVING_BELL_PT1_INSPECT,
            DIVING_BELL_PT2_BALLOW,
            DIVING_BELL_PT3_DESCEND,
            BUILDING_MATERIALS,
            BUILDING_MATERIALS_BRIDGE,
            BUILDING_MATERIALS_STATUE,
            PILGRIM_RAGS,
            MOSSBERRY_COLLECTION_PRE,
            MOSSBERRY_COLLECTION_1,
            SAVE_THE_FLEAS_PRE,
            SAVE_THE_FLEAS,
            ROCK_ROLLERS,
            SKULL_KING,
            BROLLY_GET,
            JOURNAL,
            BELLTOWN_HOUSE_START,
            BELLTOWN_HOUSE_MID,
            PINSMITHS_TOOLS,
            SHINY_BELL_GOOMBA,
            SAVE_COURIER_SHORT,
            SAVE_COURIER_TALL,
            COURIER_DELIVERY_BONEBOTTOM,
            COURIER_DELIVERY_PILGRIMS_REST,
            COURIER_DELIVERY_SONGCLAVE,
            COURIER_DELIVERY_FLEATOPIA,
            COURIER_DELIVERY_MASK_MAKER,
            COURIER_DELIVERY_DUSTPENS_SLAVE,
            COURIER_DELIVERY_FIXER,
            CROW_FEATHERS_PRE,
            CROW_FEATHERS,
            BEASTFLY_HUNT,
            SHELL_FLOWERS,
            WOOD_WITCH_CURSE,
            DOCTOR_CURSE_CURE,
            EXTRACTOR_BLUE,
            EXTRACTOR_BLUE_WORMS,
            ROACH_KILLING,
            SONGCLAVE_DONATION_1,
            SONGCLAVE_DONATION_2,
            FINE_PINS,
            SAVE_CITY_MERCHANT,
            SAVE_CITY_MERCHANT_BRIDGE,
            SONG_PILGRIM_CLOAKS,
            BROODMOTHER_HUNT,
            GREAT_GOURMAND,
            SAVE_SHERMA,
            SONG_KNIGHT,
            HUNTRESS_QUEST,
            HUNTRESS_QUEST_RUNT,
            SHAKRA_FINAL_QUEST,
            SOUL_SNARE,
            PINSTRESS_BATTLE_PRE,
            PINSTRESS_BATTLE,
            SPRINTMASTER_RACE,
            GARMOND_BLACK_THREADED,
            TORMENTED_TROBBIO,
            ANT_TRAPPER,
            FLEA_GAMES_PRE,
            FLEA_GAMES,
            STEEL_SENTINEL,
            STEEL_SENTINEL_PT2,
            MR_MUSHROOM,
            DESTROY_THREAD_CORES,
            SPRINTMASTER_PRE,
            SOUL_SNARE_PRE
        };
    }
}
