using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Silkipelago.Archipelago
{
    public class SilksongLocationChecker  : LocationChecker
    {
        static SilksongLocationChecker _instance;

        public static SilksongLocationChecker Instance
        {
            set
            {
                _instance = value;
            }
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
    }
}
