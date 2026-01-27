using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using System;
using System.Collections.Generic;

namespace Silkipelago.HarmonyPatches.Unity
{
    [HarmonyPatch(typeof(UnityConverterInitializer), "CreateConverters")]
    internal class UnityConverterInitializerPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        private static void Postfix(ref List<JsonConverter> __result)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(
                    nameof(UnityConverterInitializer),
                    "CreateConverters",
                    nameof(UnityConverterInitializerPatch),
                    nameof(Postfix)
                );

                var initialCount = __result.Count;
                var removedNames = new List<string>();

                // Collect names while removing
                int removed = __result.RemoveAll(c =>
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
                    _logger.LogInfo($"Removed {removedNames.Count} converter(s):");
                    foreach (var name in removedNames)
                    {
                        _logger.LogInfo($"Removed converterName:   - {name}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(UnityConverterInitializerPatch), nameof(Postfix), ex);
            }
        }
    }
}
