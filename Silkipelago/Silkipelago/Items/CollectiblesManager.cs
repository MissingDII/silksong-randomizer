namespace Silkipelago.Items
{
    public static class CollectiblesManager
    {
        private static void SetCollectible(string name, int amount)
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

        public static void addOneCollectible(string name)
        {
            SetCollectible(name, 1);
        }
    }
}
