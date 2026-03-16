using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;

namespace Silkipelago.context
{
    public class ArchipelagoContext
    {
        public SilksongArchipelagoClient _archipelago { get; }
        public SilksongLocationChecker _locationChecker { get; }
        public SilksongItemManager _itemManager { get; }
        public ArchipelagoContext(SilksongArchipelagoClient client, SilksongLocationChecker locationChecker, SilksongItemManager silksongItemManager)
        {
            _archipelago = client;
            _locationChecker = locationChecker;
            _itemManager = silksongItemManager;
        }
    }
}
