using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using System.Collections.Generic;

namespace Silkipelago.Archipelago
{
    public class SilksongLocationChecker : LocationChecker
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public SilksongLocationChecker(ArchipelagoClient archipelago, List<string> locationsAlreadyChecked) : base(Logger, archipelago, locationsAlreadyChecked)
        {
        }

        public override void AddCheckedLocation(string locationName)
        {
            if (!ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.Contains(locationName))
            {
                ArchipelagoPlugin.App.Logger.LogInfo("sending location for " + locationName);
                base.AddCheckedLocation(locationName);
            }
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.Add(locationName);
        }
    }
}
