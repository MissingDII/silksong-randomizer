using KaitoKid.ArchipelagoUtilities.Net.Client;
using System.Collections.Generic;

namespace Silkipelago.Settings
{
    public class SaveSettingsData
    {
        public HashSet<ReceivedItem> ProcessedItems { get; set; } = new();
        public HashSet<string> ProcessedLocations { get; set; } = new();
        public string HostName { get; set; }
        public int Port { get; set; }
        public string SlotName { get; set; }
    }

}
