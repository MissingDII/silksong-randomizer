using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using System;

namespace Silkipelago.HarmonyPatches
{
    /// <summary>
    /// Utility class providing common error handling and logging for Harmony patches.
    /// Note: Cannot be inherited by static patch classes, use as a helper instead.
    /// </summary>
    public static class BasePatch
    {
        /// <summary>
        /// Gets the logger instance from the Archipelago plugin.
        /// </summary>
        public static ILogger Logger => ArchipelagoPlugin.App.Logger;

        /// <summary>
        /// Safely executes a patch action with consistent error handling and logging.
        /// </summary>
        /// <param name="action">The action to execute that returns a bool.</param>
        /// <param name="patchName">The name of the patch class for logging.</param>
        /// <param name="methodName">The name of the method being patched for logging.</param>
        /// <returns>The result from action, or RUN_ORIGINAL_METHOD on exception.</returns>
        public static bool SafeExecute(Func<bool> action, string patchName, string methodName)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(patchName, methodName, ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        /// <summary>
        /// Safely executes a patch action without a return value.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="patchName">The name of the patch class for logging.</param>
        /// <param name="methodName">The name of the method being patched for logging.</param>
        public static void SafeExecuteVoid(Action action, string patchName, string methodName)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(patchName, methodName, ex);
            }
        }
    }
}
