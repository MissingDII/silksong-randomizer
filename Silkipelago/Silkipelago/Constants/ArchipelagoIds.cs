using System.Collections.Generic;
using static Silkipelago.Constants.PlayerDataStrings;


namespace Silkipelago.Constants
{
    public static class ArchipelagoIds
    {
        public static Dictionary<string, string> Items { get; } = new Dictionary<string, string>
    {
        { "Drifter's Cloak", HAS_DRIFTER_CLOAK },
        { "Cling Grip", HAS_WALL_JUMP },
        { "Swift Step", HAS_DASH },
        { "Faydown Cloak", HAS_DOUBLE_JUMP },
        { "Clawline", HAS_HARPOON_DASH }
    };

        public static string GetInGameName(string archipelagoName)
        {
            return Items.TryGetValue(archipelagoName, out var value) ? value : null;
        }
    }
}
