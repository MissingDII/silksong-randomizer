using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class ShrineBellHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public static void addBell(string shrineBellName)
        {
            var bellCount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            Logger.LogInfo($"Received bell number {bellCount}/5");
            PlayerDataHandler.ChangeBooleanValue(shrineBellName, true);
            var fullquestBase = QuestManager.GetQuest(QuestIds.GRAND_GATE_BELLSHRINES);
            var completion = fullquestBase.Completion;
            if (!completion.IsAccepted)
            {
                completion.IsAccepted = true;
                completion.HasBeenSeen = true;
            }
            if (bellCount >= 5)
            {
                completion.SetCompleted();
            }
            fullquestBase.Completion = completion;




        }
    }
}
