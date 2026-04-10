using Archipelago.MultiClient.Net.Helpers;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.HarmonyPatches.Hero;
using Silkipelago.Settings;
using System;

namespace Silkipelago.Archipelago
{
    public class ArchipelagoConnectionHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public bool ConnectToArchipelago(ArchipelagoConnectionInfo connectionInfo)
        {
            CreateOrOverwriteArchipelagoContext();
            return ConnectToArchipelago(() => InitializeAfterConnection(), connectionInfo);
        }

        private void CreateOrOverwriteArchipelagoContext()
        {
            var archipelagoClient = new SilksongArchipelagoClient(ArchipelagoPlugin.App.Logger, OnItemReceived);
            var locationChecker = new SilksongLocationChecker(archipelagoClient, ArchipelagoPlugin.App.Logger, []);
            var itemManager = new SilksongItemManager(archipelagoClient, []);
            ArchipelagoPlugin.App.ArchipelagoContext._archipelago = archipelagoClient;
            ArchipelagoPlugin.App.ArchipelagoContext._locationChecker = locationChecker;
            ArchipelagoPlugin.App.ArchipelagoContext._itemManager = itemManager;

        }

        private void OnItemReceived(ReceivedItemsHelper receivedItemsHelper)
        {
            var app = ArchipelagoPlugin.App;
            if (app.ArchipelagoClient == null || app.ItemManager == null || !app.ArchipelagoClient.IsConnected || !GameManager.instance.IsGameplayScene())
            {
                return;
            }

            app.ItemManager.ReceiveAllNewItems();
        }

        private void InitializeAfterConnection()
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            locationChecker.VerifyNewLocationChecksWithArchipelago();
            locationChecker.SendAllLocationChecks();
            ArchipelagoPlugin.App.ArchipelagoContext._archipelago._shouldDoInitialLoad = true;

            // Initialize cached randomization checks after connection is established
            InitializeCachedLocationChecks();
        }

        private void InitializeCachedLocationChecks()
        {
            // Initialize slash direction randomization check
            BlockSlashPatch.InitializeCachedValues();
            CanBindPatch.InitializeCachedValues();
            // remove cross stitch extra unlock condition
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
            saveSettingData.DeathLink = archipelago.SlotData.DeathLink;
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData = saveSettingData;
            actionAfterConnection?.Invoke();
            return true;
        }
    }
}
