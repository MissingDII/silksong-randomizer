namespace Silkipelago.Utils
{
    public static class PauseHelper
    {

        public static void TogglePauseGame()
        {
            //Force pause on the player
            var gameManager = GameManager.instance;
            if (PlayerData.instance.disablePause || GameManager.instance.TimeSlowed ||
                UIManager.instance.ignoreUnpause && gameManager.GetSceneNameString() != "Menu_Title" &&
                gameManager.IsGameplayScene())
            {
                GameManager.instance.timeSlowedCount = 0;
                UIManager.instance.ignoreUnpause = false;
                PlayerData.instance.disablePause = false;
                UIManager.instance.TogglePauseGame();
            }
            else
            {
                UIManager.instance.TogglePauseGame();
            }
        }
    }
}
