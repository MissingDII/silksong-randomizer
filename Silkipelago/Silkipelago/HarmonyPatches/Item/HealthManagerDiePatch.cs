using HarmonyLib;
using Silkipelago.Constants;
using System;
using System.Text.RegularExpressions;
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
            var gameObjectName = __instance.gameObject.name;
            var baseName = ExtractBaseName(gameObjectName);

            BasePatch.Logger?.LogInfo($"[HealthManager.Die] {gameObjectName} (base: {baseName}) died from attack type: {attackType}");
            if (BossIds.WIDOW.Equals(baseName))
            {
                //in case of widow defeat we send moorwing check as well
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(BossIds.MOORWING);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
            }

            if (BossIds.BOSSES.Contains(baseName))
            {
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(baseName);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
            }

            if (MonsterIds.MONSTERS.Contains(baseName))
            {
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(baseName);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
            }
            return true;
        }

        /// <summary>
        /// Extracts the base name from a GameObject name by removing Unity's clone suffix pattern (N).
        /// For example: "Monster (0)" -> "Monster", "Boss (1)" -> "Boss"
        /// </summary>
        private static string ExtractBaseName(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return objectName;

            return Regex.Replace(objectName, @"\s*\(\d+\)\s*$", "");
        }
    }
}
