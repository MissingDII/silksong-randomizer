using HarmonyLib;
using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolItem))]
    public static class ToolEquipPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(ToolItem tool)
        {
            _logger.LogInfo($"[ToolItemManager] AutoEquip called for: {tool.name}");

            // return false to block, true to allow
            return false;
        }
    }
}
