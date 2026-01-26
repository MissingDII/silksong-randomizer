using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace Silkipelago.HarmonyPatches.SaveUtility
{
    [HarmonyPatch(typeof(SaveDataUtility), "CreateJsonObjects")]
    internal class SaveSerializePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        private static void Postfix(object __instance)
        {
            _logger.LogDebugPatchIsRunning(nameof(SaveDataUtility), "CreateJsonObjects", nameof(SaveSerializePatch), nameof(Postfix));
            // Access private static field via reflection
            var serializerField = typeof(SaveDataUtility)
                .GetField("_serializer", BindingFlags.NonPublic | BindingFlags.Static);

            var serializer = serializerField?.GetValue(null) as JsonSerializer;
            if (serializer == null)
                return;

            var converters = serializer.Converters;
            for (var i = converters.Count - 1; i >= 0; i--)
            {
                if (converters[i].GetType().Name == "PermissionsEnumConverter")
                {
                    converters.RemoveAt(i);
                }
            }
        }
    }

}
