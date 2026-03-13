using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using System;

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
            try
            {
                _logger.LogInfo($"[ToolItemManager] AutoEquip called for: {tool.name}");

                // return false to block, true to allow
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(ToolEquipPatch), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
