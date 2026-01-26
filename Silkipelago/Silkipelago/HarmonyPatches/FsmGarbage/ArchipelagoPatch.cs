using System;
using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;

namespace Silkipelago.HarmonyPatches.FsmGarbage
{
    [HarmonyPatch(typeof(JsonSerializer))]
    [HarmonyPatch(nameof(JsonSerializer.Serialize), new[] { typeof(JsonWriter), typeof(object) })]
    public static class ArchipelagoPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        //  public void Serialize(JsonWriter jsonWriter, object? value)
        public static bool Prefix(JsonSerializer __instance, JsonWriter jsonWriter, object? value)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(ArchipelagoPatch), nameof(SaveDataUtility.SerializeSaveData), nameof(ArchipelagoPatch), nameof(Prefix));

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(ArchipelagoPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
