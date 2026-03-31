namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class ToolItemHandler
    {
        public static void unlockTool(string inGameName)
        {
            var tool = ToolItemManager.GetToolByName(inGameName);
            SilksongItemManager.ItemToReceive += 50;
            tool.SetUnlockedTestsComplete();
            tool.Unlock();
            SilksongItemManager.ItemToReceive = 0;
        }
    }
}
