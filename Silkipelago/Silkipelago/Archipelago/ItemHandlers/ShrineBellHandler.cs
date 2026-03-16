using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class ShrineBellHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public static void addBell(string shrineBellName)
        {
            var bellCount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            Logger.LogInfo($"Received bell number {bellCount}/5");
            var test = PlayerData.instance.QuestCompletionData.GetData("Grand Gate Bellshrines");
            PlayerDataHandler.ChangeBooleanValue(shrineBellName, true);
            QuestManager.TryGetFullQuestBase("Grand Gate Bellshrines", out var fullquestBase);
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
