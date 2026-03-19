using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using System.Collections.Generic;

namespace Silkipelago.Archipelago
{
    public class SilksongLocationChecker : LocationChecker
    {
        public SilksongLocationChecker(ArchipelagoClient archipelago, ILogger logger, List<string> locationsAlreadyChecked) : base(logger, archipelago, locationsAlreadyChecked)
        {
        }

        public override void AddCheckedLocation(string locationName)
        {
            if (!ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.Contains(locationName))
            {
                base.AddCheckedLocation(locationName);
            }
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.Add(locationName);
        }
    }
}
