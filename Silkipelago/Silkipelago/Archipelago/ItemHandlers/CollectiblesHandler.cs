using Silkipelago.Constants;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class CollectiblesHandler
    {
        private static void SetCollectible(string name)
        {
            if (CollectableItemManager.IsInHiddenMode())
            {
                CollectableItemManager.Instance.AffectItemData(name, (ref CollectableItemsData.Data data) => data.AmountWhileHidden += 1);
            }
            else
            {
                CollectableItemManager.Instance.AffectItemData(name, (ref CollectableItemsData.Data data) => data.Amount += 1);
            }
        }

        private static void AcceptQuestIfNeeded(string questId)
        {
            var quest = QuestManager.GetQuest(questId);
            var completion = quest.Completion;

            if (!completion.IsAccepted)
            {
                completion.IsAccepted = true;
                completion.HasBeenSeen = true;
                quest.Completion = completion;
            }
        }

        public static void addOneCollectible(string name)
        {
            SetCollectible(name);

            if (name == CollectablesIds.EVERBLOOM)
            {
                AcceptQuestIfNeeded(QuestIds.BLACK_THREAD_PT6_FLOWER);
            }
            else if (name == CollectablesIds.SOUL_SNARE)
            {
                AcceptQuestIfNeeded(QuestIds.SILK_DEFEAT_SNARE);
            }
        }
    }
}
