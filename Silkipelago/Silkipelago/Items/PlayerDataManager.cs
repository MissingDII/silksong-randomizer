using System;

namespace Silkipelago.Items
{
    public static class PlayerDataManager
    {
        public static void ChangeBooleanValue(string booleanName, Boolean newValue)
        {
            var instance = PlayerData.instance;
            instance.SetBool(booleanName, newValue);
        }
    }
}
