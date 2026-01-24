using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

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

        // private void SerializeValue(
        //   JsonWriter writer,
        //   object? value,
        //   JsonContract? valueContract,
        //   JsonProperty? member,
        //   JsonContainerContract? containerContract,
        //   JsonProperty? containerProperty)
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
                if (value == null)
                {
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }

                var converter = member?.Converter ?? containerProperty?.ItemConverter ?? containerContract?.ItemConverter ?? valueContract.Converter;

                var jsonSerializerInternalWriterType = AccessTools.TypeByName("JsonSerializerInternalWriter");
                // internal readonly JsonSerializer Serializer;
                var serializerField = jsonSerializerInternalWriterType.GetField("Serializer", BindingFlags.NonPublic | BindingFlags.Instance);
                var serializerValue = serializerField.GetValue(__instance);

                // internal JsonConverter? GetMatchingConverter(Type type)
                var JsonSerializerType = typeof(JsonSerializer);
                var getMatchingConverterMethod = JsonSerializerType.GetMethod("GetMatchingConverter", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Type) }, null);

                var matchingConverter = getMatchingConverterMethod.Invoke(serializerValue, new []{valueContract.UnderlyingType}) as JsonConverter;

                converter = matchingConverter ?? valueContract.InternalConverter;

                if (converter != null && converter.CanWrite)
                {
                    _logger.LogInfo("new value for converter");
                    _logger.LogInfo(value?.ToString() ?? "null");
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
                _logger.LogErrorException(nameof(ArchipelagoPatch2), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
