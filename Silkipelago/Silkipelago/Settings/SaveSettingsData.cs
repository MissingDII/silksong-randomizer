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

        public bool DeathLink { get; set; }

        public bool DownSlash { get; set; }
        public bool UpSlash { get; set; }
        public bool LeftSlash { get; set; }
        public bool RightSlash { get; set; }

        public bool Bind { get; set; }

        public int SavedFleas { get; set; }
    }

}
