using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using KaitoKid.ArchipelagoUtilities.Net.Json;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.IdTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Silkipelago.Archipelago
{
    public class SilksongArchipelagoClient : ArchipelagoClient
    {
        private static IJsonLoader _jsonLoader = new NewtonsoftJsonLoader();
        private static SilksongArchipelagoClient _instance;

        public override string GameName => "Silksong";
        public override string ModName => "Silkipelago";
        public override string ModVersion => MyPluginInfo.PLUGIN_VERSION;

        public SilksongSlotData SlotData => (SilksongSlotData)_slotData;


        public static SilksongArchipelagoClient Instance
        {
            set => _instance = value;
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("SilksongArchipelagoClient not initialized. Set Instance first.");
                }
                return _instance;
            }
        }

        public SilksongArchipelagoClient(ILogger logger, Action<ReceivedItemsHelper> itemReceivedFunction) :
            base(logger, new DataPackageCache(new ArchipelagoItemLoader(_jsonLoader), new SilksongLocationLoader(logger, _jsonLoader), "silksong", "BepInEx", "plugins", "Silkipelago", "IdTables"), itemReceivedFunction)
        {
        }

        protected override void InitializeSlotData(string slotName, Dictionary<string, object> slotDataFields)
        {
            _slotData = new SilksongSlotData(slotName, slotDataFields, Logger);
        }

        protected override void OnPacketReceived(ArchipelagoPacketBase packet)
        {

        }

        protected override void OnMessageReceived(LogMessage message)
        {
            var fullMessage = string.Join(" ", message.Parts.Select(str => str.Text));
            Logger.LogInfo(fullMessage);
        }

        protected override void KillPlayerDeathLink(DeathLink deathLinkOptions)
        {
            Logger.LogInfo($"Receiving Death Link from {deathLinkOptions.Source} ({deathLinkOptions.Cause})");
        }
    }
}
