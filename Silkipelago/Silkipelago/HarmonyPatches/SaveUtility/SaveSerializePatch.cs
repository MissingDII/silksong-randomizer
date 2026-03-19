using HarmonyLib;
using Newtonsoft.Json;
using System.Reflection;

namespace Silkipelago.HarmonyPatches.SaveUtility
{
    /// <summary>
    /// Patches SaveDataUtility to remove problematic JSON converters.
    /// </summary>
    [HarmonyPatch(typeof(SaveDataUtility), "CreateJsonObjects")]
    internal class SaveSerializePatch
    {
        /// <summary>
        /// Postfix that removes unsupported JSON converters after serializer creation.
        /// </summary>
        private static void Postfix(object __instance)
        {
            BasePatch.SafeExecuteVoid(
                () => RemoveUnsupportedConverters(),
                nameof(SaveSerializePatch),
                nameof(Postfix)
            );
        }

        private static void RemoveUnsupportedConverters()
        {
            BasePatch.Logger.LogDebugPatchIsRunning(nameof(SaveDataUtility), "CreateJsonObjects", nameof(SaveSerializePatch), nameof(Postfix));

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
