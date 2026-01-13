using HarmonyLib;
using HutongGames.PlayMaker;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements;

namespace Silkipelago.HarmonyPatches.FsmGarbage
{

    [HarmonyPatch(typeof(PlayMakerFSM))]
    [HarmonyPatch(nameof(PlayMakerFSM.SendEvent))]
    public class FsmPatcher
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        public static bool Prefix(PlayMakerFSM __instance, string eventName)
        {
            if (__instance != null && eventName != null)
            {
                //_logger.LogDebug($"[FSM LOG] FSM: {__instance.FsmName}, GO: {__instance.gameObject.name}, Event: {eventName}");
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}

