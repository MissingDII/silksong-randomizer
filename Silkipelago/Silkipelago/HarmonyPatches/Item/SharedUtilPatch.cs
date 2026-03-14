using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using System;
using TeamCherry.SharedUtils;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(VariableExtensions))]
    [HarmonyPatch(nameof(VariableExtensions.SetVariable), new Type[] { typeof(IIncludeVariableExtensions), typeof(string), typeof(object), typeof(Type) })]
    public static class SharedUtilPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(IIncludeVariableExtensions obj, string fieldName, object value, Type type)
        {
            return PlayerDataPatchHelper.ExecutePatchLogic(_logger, nameof(SharedUtilPatch), nameof(Prefix), () =>
            {
                if (obj != PlayerData.instance)
                    return MethodPrefix.RUN_ORIGINAL_METHOD;

                return PlayerDataPatchHelper.HandlePlayerDataFieldChange(fieldName, ArchipelagoPlugin.App.LocationChecker);
            }, MethodPrefix.RUN_ORIGINAL_METHOD);
        }
    }
}


