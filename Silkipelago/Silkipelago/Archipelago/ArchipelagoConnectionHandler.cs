using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.Archipelago
{
    public class ArchipelagoConnectionHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public bool ConnectToArchipelago(ArchipelagoConnectionInfo connectionInfo)
        {
            return ConnectToArchipelago(() => InitializeAfterConnection(), connectionInfo);
        }

        private void InitializeAfterConnection()
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            locationChecker.VerifyNewLocationChecksWithArchipelago();
            locationChecker.SendAllLocationChecks();
            ArchipelagoPlugin.App.ArchipelagoContext._archipelago._shouldDoInitialLoad = true;
        }

        private bool ConnectToArchipelago(Action actionAfterConnection, ArchipelagoConnectionInfo connectionInfo)
        {
            var archipelago = ArchipelagoPlugin.App.ArchipelagoClient;

            if (connectionInfo == null)
            {
                Logger.LogMessage($"Tried to connect, but no information provided!");
                return false;
            }

            if (archipelago.IsConnected)
            {
                Logger.LogMessage($"Tried to connect, but already connected!");
                return false;
            }

            var connectionResult = archipelago.ConnectToMultiworld(connectionInfo);
            if (!connectionResult.Success || !archipelago.IsConnected)
            {
                connectionInfo = null;
                var userMessage =
                    $"Could not connect to archipelago.{Environment.NewLine}Message: {connectionResult.Message}{Environment.NewLine}Please verify the connection info and that the server is available.{Environment.NewLine}";
                Logger.LogError(userMessage);
                return false;
            }

            Logger.LogMessage($"Connected to Archipelago as {archipelago.SlotData.SlotName}.");
            //Saving connection info to global save

            var saveSettingData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData != null ? ArchipelagoPlugin.App.SettingsContext.saveSettingsData : new SaveSettingsData();
            saveSettingData.HostName = connectionInfo.HostUrl;
            saveSettingData.Port = connectionInfo.Port;
            saveSettingData.SlotName = connectionInfo.SlotName;
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData = saveSettingData;
            actionAfterConnection?.Invoke();
            return true;
        }
    }
}
