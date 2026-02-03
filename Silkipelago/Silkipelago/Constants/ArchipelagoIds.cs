using System.Collections.Generic;
using System.Linq;
using static Silkipelago.Constants.PlayerDataStrings;


namespace Silkipelago.Constants
{
    public static class ArchipelagoIds
    {
        public static Dictionary<string, string> ArchipelagoIdsToGameIds { get; } = new Dictionary<string, string>
    {
        //abilities
        { "Drifter's Cloak", HAS_DRIFTER_CLOAK },
        { "Cling Grip", HAS_WALL_JUMP },
        { "Swift Step", HAS_DASH },
        { "Faydown Cloak", HAS_DOUBLE_JUMP },
        { "Clawline", HAS_HARPOON_DASH }
        //bosses
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
