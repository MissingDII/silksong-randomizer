using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;

namespace Silkipelago.HarmonyPatches.Hero
{
    [HarmonyPatch(typeof(HeroController))]
    [HarmonyPatch(nameof(HeroController.CanBind))]
    public class CanBindPatch
    {
        // Cache the randomization check result - initialized during plugin startup
        private static bool isBindRandomized;

        /// <summary>
        /// Intercepts the CanBind method to handle bind randomization.
        /// - If bind is randomized and disabled: prevents binding by returning false
        /// - Otherwise: lets the original method run
        /// </summary>
        public static bool Prefix(HeroController __instance, out bool __result)
        {
            if (isBindRandomized)
            {
                var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                if (!saveData.Bind)
                {
                    __result = false;
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD; // Skip original, return false
                }
            }

            __result = default; // Will be overwritten by original method
            return MethodPrefix.RUN_ORIGINAL_METHOD; // Run original method, let it set the return value
        }

        public static void InitializeCachedValues()
        {
            if (!ArchipelagoPlugin.App.ArchipelagoClient.SlotData.StartingBind)
            {
                isBindRandomized = true;
            }
        }
    }
}
