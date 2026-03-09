using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using System;
using System.Collections.Generic;

namespace Silkipelago.Archipelago
{
    public class SilksongLocationChecker : LocationChecker
    {
        private static SilksongLocationChecker _instance;

        public static SilksongLocationChecker Instance
        {
            set => _instance = value;
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("SilksongLocationChecker not initialized. Set instance first.");
                }
                return _instance;
            }
        }

        public SilksongLocationChecker(ILogger logger, ArchipelagoClient archipelago, List<string> locationsAlreadyChecked) : base(logger, archipelago, locationsAlreadyChecked)
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
