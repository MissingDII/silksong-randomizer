using System.Collections.Generic;
using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;

namespace Silkipelago.Items
{
    public class SilksongItemManager : ItemManager
    {
        private ILogger _logger;

        public SilksongItemManager(ILogger logger, SilksongArchipelagoClient archipelago,
            IEnumerable<ReceivedItem> itemsAlreadyProcessed) : base(archipelago, itemsAlreadyProcessed)
        {
            _logger = logger;
        }

        protected override void ProcessItem(ReceivedItem receivedItem, bool immediatelyIfPossible)
        {
            //if (TryHandleReceivedPerk(receivedItem))
            //{
            //    return;
            //}

            //if (_trapExecutor.TryHandleReceivedTrap(receivedItem))
            //{
            //    return;
            //}
        }
    }
}
