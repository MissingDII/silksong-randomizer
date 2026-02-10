using System;

namespace Silkipelago.Items
{
    public static class PlayerDataManager
    {
        public static void ChangeBooleanValue(string booleanName, Boolean newValue)
        {
            var instance = PlayerData.instance;
            SilksongItemManager._itemToReceive++;
            instance.SetBool(booleanName, newValue);
        }
    }
}
