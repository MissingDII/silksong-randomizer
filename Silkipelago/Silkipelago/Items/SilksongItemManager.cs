//using KaitoKid.ArchipelagoUtilities.Net;
//using KaitoKid.ArchipelagoUtilities.Net.Client;
//using KaitoKid.Utilities.Interfaces;
//using Silkipelago.Archipelago;
//using System;
//using System.Collections.Generic;

//namespace Silkipelago.Items
//{
//    public class SilksongItemManager : ItemManager
//    {
//        private ILogger _logger;
//        private static SilksongItemManager _instance;

//        public static SilksongItemManager Instance
//        {
//            set => _instance = value;
//            get
//            {
//                if (_instance == null)
//                {
//                    throw new InvalidOperationException("SilksongItemManager not initialized. Set instance first.");
//                }
//                return _instance;
//            }
//        }

//        public SilksongItemManager(ILogger logger, SilksongArchipelagoClient archipelago,
//            IEnumerable<ReceivedItem> itemsAlreadyProcessed) : base(archipelago, itemsAlreadyProcessed)
//        {
//            _logger = logger;
//        }

//        protected override void ProcessItem(ReceivedItem receivedItem, bool immediatelyIfPossible)
//        {
//            //if (TryHandleReceivedPerk(receivedItem))
//            //{
//            //    return;
//            //}

//            //if (_trapExecutor.TryHandleReceivedTrap(receivedItem))
//            //{
//            //    return;
//            //}
//        }
//    }
//}
