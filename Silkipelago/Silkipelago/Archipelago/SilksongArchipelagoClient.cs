using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Helpers;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.Archipelago
{
    public class SilksongArchipelagoClient : ArchipelagoClient
    {
        public override string GameName => "Dungeon Clawler";
        public override string ModName => "Clawrchipelago";
        public override string ModVersion => MyPluginInfo.PLUGIN_VERSION;

        public SilksongSlotData SlotData => (SilksongSlotData)_slotData;

        public SilksongArchipelagoClient(ILogger logger, Action<ReceivedItemsHelper> itemReceivedFunction) :
            base(logger, new DataPackageCache("silksong", "BepInEx", "plugins", "Silkipelago", "IdTables"), itemReceivedFunction)
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
            // DeathlinkPatches.ReceiveDeathink();
        }
    }
}
