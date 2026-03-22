using Silkipelago.Archipelago;
using Silkipelago.Archipelago.UI;

namespace Silkipelago.context
{
    public class UIContext
    {
        public ArchipelagoMenuUI MenuUI { get; }
        public ArchipelagoConnectionHandler ConnectionHandler { get; }
        public ItemNotificationUI ItemNotification { get; }

        public UIContext(ArchipelagoMenuUI menuUI, ArchipelagoConnectionHandler connectionHandler)
        {
            MenuUI = menuUI;
            ConnectionHandler = connectionHandler;
            ItemNotification = new ItemNotificationUI();
        }
    }
}
