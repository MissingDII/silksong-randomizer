using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System.Linq;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class ShrineBellHandler
    {
        private const int MAX_BELLS = 5;
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void AddBell(string shrineBellName)
        {
            var bellCount = CountReceivedBells();
            Logger.LogInfo($"Received bell number {bellCount}/{MAX_BELLS}");
            PlayerDataHandler.ChangeBooleanValue(shrineBellName, true);
            UpdateQuestCompletion(bellCount);
        }

        private static int CountReceivedBells()
        {
            return PlayerDataIds.SHRINES
                .Select(bell => ArchipelagoItemIds.GetArchipelagoName(bell))
                .Count(itemId => ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount(itemId) > 0);
        }

        private static void UpdateQuestCompletion(int bellCount)
        {
            var quest = QuestManager.GetQuest(QuestIds.GRAND_GATE_BELLSHRINES);
            var completion = quest.Completion;

            if (completion.IsAccepted)
            {
                if (bellCount >= MAX_BELLS)
                {
                    completion.SetCompleted();
                }
            }
            quest.Completion = completion;
        }
    }
}
