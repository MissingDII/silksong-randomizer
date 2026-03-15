namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class ToolItemHandler
    {
        public static void unlockTool(string inGameName)
        {
            var tool = ToolItemManager.GetToolByName(inGameName);
            tool.SetUnlockedTestsComplete();
            tool.Unlock();
        }
    }
}
