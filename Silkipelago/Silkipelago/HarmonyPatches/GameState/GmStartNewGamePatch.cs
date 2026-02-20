using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.StartNewGame))]
    public class GmStartNewGamePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }


        //  public void StartNewGame(bool permaDeath = false, bool bossRush = false)
        public static void Postfix(GameManager __instance, bool permadeathMode, bool bossRushMode)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(GameManager), nameof(GameManager.StartNewGame), nameof(GmStartNewGamePatch), nameof(Postfix));
                SaveSettings.ClearSaveData(__instance.profileID);
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(GmStartNewGamePatch), nameof(Postfix), ex);
            }
        }
    }
}
