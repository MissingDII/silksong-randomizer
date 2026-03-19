using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using System;
using System.Collections.Generic;

namespace Silkipelago.HarmonyPatches.Unity
{
    [HarmonyPatch(typeof(UnityConverterInitializer), "CreateConverters")]
    internal class UnityConverterInitializerPatch
    {
        private static void Postfix(ref List<JsonConverter> __result)
        {
            try
            {
                BasePatch.Logger.LogDebugPatchIsRunning(
                    nameof(UnityConverterInitializer),
                    "CreateConverters",
                    nameof(UnityConverterInitializerPatch),
                    nameof(Postfix)
                );

                var initialCount = __result.Count;
                var removedNames = new List<string>();

                // Collect names while removing
                var removed = __result.RemoveAll(c =>
                {
                    var typeName = c.GetType().Name;
                    if (typeName.Equals("PermissionsEnumConverter"))
                    {
                        removedNames.Add(typeName);
                        return true;
                    }
                    return false;
                });

                //log removed converters
                if (removedNames.Count > 0)
                {
                    BasePatch.Logger.LogInfo($"Removed {removedNames.Count} converter(s):");
                    foreach (var name in removedNames)
                    {
                        BasePatch.Logger.LogInfo($"Removed converterName:   - {name}");
                    }
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(UnityConverterInitializerPatch), nameof(Postfix), ex);
            }
        }
    }
}
