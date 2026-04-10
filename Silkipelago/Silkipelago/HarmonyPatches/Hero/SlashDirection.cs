using GlobalEnums;
using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago.SlotData;

namespace Silkipelago.HarmonyPatches.Hero
{
    [HarmonyPatch(typeof(HeroController))]
    [HarmonyPatch(nameof(HeroController.Attack))]
    public class BlockSlashPatch
    {

        // Cache the randomization check result - initialized during archipelago connection
        private static bool isSlashRandomized;

        public static bool Prefix(HeroController __instance, AttackDirection attackDir)
        {
            return BasePatch.SafeExecute(() => HandleSlashAttack(__instance, attackDir), nameof(BlockSlashPatch), nameof(Prefix));
        }

        private static bool HandleSlashAttack(HeroController instance, AttackDirection attackDir)
        {
            if (!isSlashRandomized)
                return MethodPrefix.RUN_ORIGINAL_METHOD;

            return IsSlashAllowed(instance, attackDir);
        }

        /// <summary>
        /// Initializes the cached randomization check after archipelago connection.
        /// Call this once during plugin startup to perform the expensive network check.
        /// </summary>
        public static void InitializeCachedValues()
        {
            if (!ArchipelagoPlugin.App.ArchipelagoClient.SlotData.SlashRandomized.Equals(RandomizeSlash.All))
            {
                isSlashRandomized = true;
            }
        }

        private static bool IsSlashAllowed(HeroController instance, AttackDirection attackDir)
        {
            var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            return attackDir switch
            {
                AttackDirection.upward => saveData.UpSlash,
                AttackDirection.downward => saveData.DownSlash,
                AttackDirection.normal => instance.cState.facingRight ? saveData.RightSlash : saveData.LeftSlash,
                _ => MethodPrefix.RUN_ORIGINAL_METHOD
            };
        }
    }
}
