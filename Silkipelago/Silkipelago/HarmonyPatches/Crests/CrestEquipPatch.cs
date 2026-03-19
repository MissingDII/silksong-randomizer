using HarmonyLib;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Crest
{
    /// <summary>
    /// Prevents auto-equipping of crests when they are randomized in Archipelago.
    /// </summary>
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolCrest), typeof(bool), typeof(bool))]
    public static class CrestEquipPatch
    {
        /// <summary>
        /// Prefix that blocks auto-equip if the crest is randomized.
        /// </summary>
        static bool Prefix(ToolCrest crest, bool markTemp, bool removeTools)
        {
            return BasePatch.SafeExecute(
                () => ShouldBlockCrestAutoEquip(crest) 
                    ? KaitoKid.ArchipelagoUtilities.Net.Constants.MethodPrefix.DONT_RUN_ORIGINAL_METHOD 
                    : KaitoKid.ArchipelagoUtilities.Net.Constants.MethodPrefix.RUN_ORIGINAL_METHOD,
                nameof(CrestEquipPatch),
                nameof(Prefix)
            );
        }

        /// <summary>
        /// Determines if crest auto-equip should be blocked based on randomization status.
        /// </summary>
        private static bool ShouldBlockCrestAutoEquip(ToolCrest crest)
        {
            BasePatch.Logger.LogInfo($"[ToolItemManager] AutoEquip called for Crest: {crest.name}");
            return IsEvaCrestUpgradeRandomized(crest) || IsBasicCrest(crest);
        }

        /// <summary>
        /// Checks if Eva crest upgrades are randomized and this is an upgrade crest.
        /// </summary>
        private static bool IsEvaCrestUpgradeRandomized(ToolCrest crest)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            return locationChecker.LocationExists(LocationConstants.EvaUpgradeLocation) 
                && CrestIds.CRESTS_UPGRADE.Contains(crest.name);
        }

        /// <summary>
        /// Checks if this is a basic/bound crest (not an upgrade).
        /// </summary>
        private static bool IsBasicCrest(ToolCrest crest)
        {
            return CrestIds.CRESTS.Contains(crest.name);
        }
    }
}
