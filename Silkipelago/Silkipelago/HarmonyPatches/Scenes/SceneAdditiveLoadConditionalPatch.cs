using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches.Scenes
{
    [HarmonyPatch(typeof(SceneAdditiveLoadConditional))]
    [HarmonyPatch(nameof(SceneAdditiveLoadConditional.ShouldLoadBoss), MethodType.Getter)]
    public class SceneAdditiveLoadConditionalPatch
    {
        public static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void Postfix(ref bool __result)
        {
            try
            {
                var currentScene = SceneManager.GetActiveScene().name;
                if (currentScene == SceneNames.Greymoor_08)
                {
                    HandleMoorwing(ref __result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SceneAdditiveLoadConditionalPatch), nameof(Postfix), ex);
            }
        }

        private static void HandleMoorwing(ref bool result)
        {
            Logger.LogInfo($"ShouldLoadBoss getter called - Original result: {result}");
            var location = ArchipelagoLocationIds.GetArchipelagoName(PlayerDataIds.VAMPIRE_GNAT_BOSS_DEFEATED);
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            if (!locationChecker.IsLocationChecked(location))
            {
                result = true;
            }
        }
    }
}
