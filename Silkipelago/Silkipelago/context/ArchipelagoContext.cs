using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;

namespace Silkipelago.context
{
    public class ArchipelagoContext
    {
        public SilksongArchipelagoClient _archipelago { get; set; }
        public SilksongLocationChecker _locationChecker { get; set; }
        public SilksongItemManager _itemManager { get; set; }
        public ArchipelagoContext()
        {
        }
    }
}
