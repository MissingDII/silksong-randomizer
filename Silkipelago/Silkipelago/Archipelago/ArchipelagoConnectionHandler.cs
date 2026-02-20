

using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.HarmonyPatches;
using Silkipelago.Items;
using Silkipelago.Settings;
using System;

namespace Silkipelago.Archipelago
{
    public static class ArchipelagoConnectionHandler
    {
        private static ILogger _logger;
        private static Harmony _harmony;
        private static SilksongArchipelagoClient _archipelagoClient;
        private static SilksongLocationChecker _silksongLocationChecker;

        public static void Init(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            _logger = logger;
            _harmony = harmony;
            _archipelagoClient = archipelagoClient;
            _silksongLocationChecker = silksongLocationChecker;
        }

        public static bool ConnectToArchipelago(ArchipelagoConnectionInfo connectionInfo)
        {
            return ConnectToArchipelago(() => InitializeAfterConnection(), connectionInfo);
        }

        private static void InitializeAfterConnection()
        {
            var locationChecker = SilksongLocationChecker.Instance;
            var itemManager = SilksongItemManager.Instance;
            var archipelago = SilksongArchipelagoClient.Instance;

            locationChecker.VerifyNewLocationChecksWithArchipelago();
            locationChecker.SendAllLocationChecks();
            var patchInitializer = new PatchInitializer();
            patchInitializer.InitializeConnectedPatches(_logger, _harmony, _archipelagoClient, _silksongLocationChecker);
            _archipelagoClient._shouldDoInitialLoad = true;
        }

        private static bool ConnectToArchipelago(Action actionAfterConnection, ArchipelagoConnectionInfo connectionInfo)
        {
            var archipelago = SilksongArchipelagoClient.Instance;

            if (connectionInfo == null)
            {
                _logger.LogMessage($"Tried to connect, but no information provided!");
                return false;
            }

            if (archipelago.IsConnected)
            {
                _logger.LogMessage($"Tried to connect, but already connected!");
                return false;
            }

            var connectionResult = archipelago.ConnectToMultiworld(connectionInfo);
            if (!connectionResult.Success || !archipelago.IsConnected)
            {
                connectionInfo = null;
                var userMessage =
                    $"Could not connect to archipelago.{Environment.NewLine}Message: {connectionResult.Message}{Environment.NewLine}Please verify the connection info and that the server is available.{Environment.NewLine}";
                _logger.LogError(userMessage);
                return false;
            }

            _logger.LogMessage($"Connected to Archipelago as {archipelago.SlotData.SlotName}.");
            //Saving connection info to global save
            var saveSettingData = GlobalSaveSettingsData.saveSettingsData != null ? GlobalSaveSettingsData.saveSettingsData : new SaveSettingsData();
            saveSettingData.HostName = connectionInfo.HostUrl;
            saveSettingData.Port = connectionInfo.Port;
            saveSettingData.SlotName = connectionInfo.SlotName;
            GlobalSaveSettingsData.saveSettingsData = saveSettingData;
            actionAfterConnection?.Invoke();
            return true;
        }
    }
}
