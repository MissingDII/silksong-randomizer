using HarmonyLib;
using System;

namespace Silkipelago.HarmonyPatches.Hero
{
    [HarmonyPatch(typeof(HeroController))]
    [HarmonyPatch(nameof(HeroController.Die), new Type[] { typeof(bool), typeof(bool) })]
    public static class DiePatch
    {
        public static bool receivedDeathLink = false;
        /// <summary>
        /// Sends a death link to archipelago when the player dies.
        /// Uses "Skill issue" as the cause of death.
        /// </summary>
        public static void Prefix(HeroController __instance, bool nonLethal, bool frostDeath)
        {
            BasePatch.SafeExecuteVoid(() => HandleDeath(nonLethal, frostDeath), nameof(DiePatch), nameof(Prefix));
        }

        private static void HandleDeath(bool nonLethal, bool frostDeath)
        {
            var deathlink = ArchipelagoPlugin.App.ArchipelagoClient.DeathLink;
            if (!receivedDeathLink && !nonLethal && deathlink)
            {
                try
                {
                    ArchipelagoPlugin.App.ArchipelagoClient.SendDeathLink("Silk issue");
                }
                catch (Exception ex)
                {
                    BasePatch.Logger.LogErrorException(nameof(DiePatch), nameof(HandleDeath), ex);
                }
            }
            receivedDeathLink = false;
        }
    }
}
