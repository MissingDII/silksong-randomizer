using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Silkipelago.HarmonyPatches.SaveUtility
{
    [HarmonyPatch(typeof(SaveDataUtility), "CreateJsonObjects")]
    internal class SaveSerializePatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        private static void Postfix(object __instance)
        {
            try
            {
                Logger.LogDebugPatchIsRunning(nameof(SaveDataUtility), "CreateJsonObjects", nameof(SaveSerializePatch), nameof(Postfix));
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
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SaveSerializePatch), nameof(Postfix), ex);
            }
        }
    }

}
