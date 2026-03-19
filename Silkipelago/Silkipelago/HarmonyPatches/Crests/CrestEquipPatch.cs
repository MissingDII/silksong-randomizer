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
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(ToolCrest crest, bool markTemp, bool removeTools)
        {
            try
            {
                Logger.LogInfo($"[ToolItemManager] AutoEquip called for Crest: {crest.name}");

                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                var isEvaCrestRandomized = locationChecker.LocationExists("Eva: 0 Slots") && CrestIds.CRESTS_UPGRADE.Contains(crest.name);
                var isCrest = CrestIds.CRESTS.Contains(crest.name);

                if (isEvaCrestRandomized || isCrest)
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(CrestEquipPatch), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
