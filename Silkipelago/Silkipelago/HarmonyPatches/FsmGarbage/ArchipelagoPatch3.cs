using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Silkipelago.HarmonyPatches.FsmGarbage
{
    [HarmonyPatch("Newtonsoft.Json.Serialization.JsonSerializerInternalWriter, Newtonsoft.Json", "SerializeValue")]
    public static class ArchipelagoPatch3
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static bool Prefix(
            object __instance,
            JsonWriter writer,
            object? value,
            JsonContract? valueContract,
            JsonProperty? member,
            JsonContainerContract? containerContract,
            JsonProperty? containerProperty)
        {
            try
            {
                if (value == null || valueContract == null)
                {
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                var converter = member?.Converter ?? containerProperty?.ItemConverter ?? containerContract?.ItemConverter ?? valueContract.Converter;

                // Get the JsonSerializerInternalWriter type with full assembly name
                var writerType = AccessTools.TypeByName("Newtonsoft.Json.Serialization.JsonSerializerInternalWriter, Newtonsoft.Json");
                if (writerType == null)
                {
                    _logger.LogWarning("Could not find JsonSerializerInternalWriter type");
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                // Get the Serializer field
                var serializerField = AccessTools.Field(writerType, "Serializer");
                if (serializerField == null)
                {
                    _logger.LogWarning("Could not find Serializer field");
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                var serializerValue = serializerField.GetValue(__instance);
                if (serializerValue == null)
                {
                    _logger.LogWarning("Serializer field value is null");
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                // Try to get the matching converter method
                var JsonSerializerType = typeof(JsonSerializer);
                var getMatchingConverterMethod = AccessTools.Method(
                    JsonSerializerType,
                    "GetMatchingConverter",
                    new[] { typeof(Type) }
                );

                if (getMatchingConverterMethod == null)
                {
                    _logger.LogWarning("Could not find GetMatchingConverter method on JsonSerializer");
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                try
                {
                    var matchingConverter = getMatchingConverterMethod.Invoke(serializerValue, new object[] { valueContract.UnderlyingType }) as JsonConverter;
                    converter = matchingConverter ?? valueContract.InternalConverter;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Failed to invoke GetMatchingConverter: {ex.Message}");
                    converter = valueContract.InternalConverter;
                }

                if (converter != null && converter.CanWrite)
                {
                    _logger.LogInfo("Converter found:");
                    _logger.LogInfo($"  Type: {converter.GetType().Name}");
                    _logger.LogInfo($"  Value: {value?.ToString() ?? "null"}");
                    _logger.LogDebugPatchIsRunning(
                        nameof(ArchipelagoPatch3),
                        "SerializeValue",
                        nameof(ArchipelagoPatch3),
                        nameof(Prefix)
                    );
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(ArchipelagoPatch3), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
