using HarmonyLib;
using Silkipelago.Constants;
using System;
using UnityEngine;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(HealthManager))]
    [HarmonyPatch(nameof(HealthManager.Die), new Type[] {
        typeof(float?),
        typeof(AttackTypes),
        typeof(NailElements),
        typeof(GameObject),
        typeof(bool),
        typeof(float),
        typeof(bool),
        typeof(bool)
    })]
    public static class HealthManagerDiePatch
    {
        static bool Prefix(
            HealthManager __instance,
            float? attackDirection,
            AttackTypes attackType,
            NailElements nailElement,
            GameObject damageSource,
            bool ignoreEvasion = false,
            float corpseFlingMultiplier = 1f,
            bool overrideSpecialDeath = false,
            bool disallowDropFling = false)
        {
            return BasePatch.SafeExecute(() => HandleDie(__instance, attackType), nameof(HealthManagerDiePatch), nameof(Prefix));
        }

        private static bool HandleDie(HealthManager __instance, AttackTypes attackType)
        {

            BasePatch.Logger?.LogInfo($"[HealthManager.Die] {__instance.gameObject.name} died from attack type: {attackType}");

            if (BossIds.BOSSES.Contains(__instance.gameObject.name))
            {
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.gameObject.name);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
            }
            return true;
        }
    }
}
