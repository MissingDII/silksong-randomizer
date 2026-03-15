using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Crest
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolCrest), typeof(bool), typeof(bool))]
    public static class CrestEquipPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(ToolCrest crest, bool markTemp, bool removeTools)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                _logger.LogInfo($"[ToolItemManager] AutoEquip called for Crest: {crest.name}");

                // If eva crest upgrades are randomized, don't auto-equip
                if (locationChecker.LocationExists("Eva: 0 Slots"))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(CrestEquipPatch), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
