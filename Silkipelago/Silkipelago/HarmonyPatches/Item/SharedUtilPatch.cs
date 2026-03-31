using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago.ItemHandlers;
using System;
using TeamCherry.SharedUtils;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(VariableExtensions))]
    [HarmonyPatch(nameof(VariableExtensions.SetVariable), new Type[] { typeof(IIncludeVariableExtensions), typeof(string), typeof(object), typeof(Type) })]
    public static class SharedUtilPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(IIncludeVariableExtensions obj, string fieldName, object value, Type type)
        {
            return PlayerDataPatchHelper.ExecutePatchLogic(nameof(SharedUtilPatch), nameof(Prefix), () =>
            {
                if (obj != PlayerData.instance)
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                if (SilksongItemManager.ItemToReceive == 0)
                {
                    return PlayerDataPatchHelper.HandlePlayerDataFieldChange(fieldName, ArchipelagoPlugin.App.LocationChecker);
                }
                SilksongItemManager.ItemToReceive--;
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }, MethodPrefix.RUN_ORIGINAL_METHOD);
        }
    }
}


