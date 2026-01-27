using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Silkipelago.HarmonyPatches.FsmGarbage
{
    [HarmonyPatch("Newtonsoft.Json.Serialization.JsonSerializerInternalWriter, Newtonsoft.Json", "SerializeConvertable")]
    public static class ArchipelagoPatch2
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static bool Prefix(
            object __instance,
            JsonWriter writer,
            JsonConverter converter,
            object value,
            JsonContract contract,
            object collectionContract,
            object containerProperty)
        {
            try
            {
                //_logger.LogInfo("new value for converter");
                //_logger.LogInfo(value?.ToString() ?? "null");
                //_logger.LogDebugPatchIsRunning(
                //    nameof(ArchipelagoPatch2),
                //    "SerializeConvertable",
                //    nameof(ArchipelagoPatch2),
                //    nameof(Prefix)
                //);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(ArchipelagoPatch2), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
