using HarmonyLib;
using System;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

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
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

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
            try
            {
                // Log enemy death
                _logger?.LogInfo($"[HealthManager.Die] {__instance.gameObject.name} died from attack type: {attackType}");

                // TODO: Add check for bosses where setBool is not called
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogErrorException(nameof(HealthManagerDiePatch), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
