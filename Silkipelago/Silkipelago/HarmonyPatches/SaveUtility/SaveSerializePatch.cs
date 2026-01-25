using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using Silkipelago.HarmonyPatches.Item;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Silkipelago.HarmonyPatches.SaveUtility
{
    [HarmonyPatch(typeof(SaveDataUtility), "CreateJsonObjects")]
    class SaveSerializePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        static void Postfix(object __instance)
        {
            _logger.LogDebugPatchIsRunning(nameof(SaveDataUtility), "CreateJsonObjects", nameof(SaveSerializePatch), nameof(Postfix));
            // Access private static field via reflection
            var serializerField = typeof(SaveDataUtility)
                .GetField("_serializer", BindingFlags.NonPublic | BindingFlags.Static);

            var serializer = serializerField?.GetValue(null) as JsonSerializer;
            if (serializer == null)
                return;

            var converters = serializer.Converters;
            for (int i = converters.Count - 1; i >= 0; i--)
            {
                if (converters[i].GetType().Name == "PermissionsEnumConverter")
                {
                    converters.RemoveAt(i);
                }
            }
        }
    }

}
