using GlobalEnums;

namespace Silkipelago.Utils
{
    static class SceneLoader
    {

        public static void LoadScene(string sceneName,string entryGateName, GatePosition position) {
            GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
            {
                SceneName = sceneName, // scene you want to go to
                EntryGateName = entryGateName, // where you want to enter from
                HeroLeaveDirection = position, // how are you leaving
                EntryDelay = 0,
                WaitForSceneTransitionCameraFade = true,
                PreventCameraFadeOut = false,
                Visualization = GameManager.SceneLoadVisualizations.Default,
                AlwaysUnloadUnusedAssets = false,
                ForceWaitFetch = false
            });
        }

        // Load with fade effect
        public static void LoadSceneWithFade(string sceneName, string entryGateName, GatePosition position)
        {
            GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
            {
                SceneName = sceneName,
                EntryGateName = entryGateName,
                HeroLeaveDirection = position,
                EntryDelay = 0.5f, // Add delay for fade effect
                WaitForSceneTransitionCameraFade = true,
                PreventCameraFadeOut = false,
                Visualization = GameManager.SceneLoadVisualizations.Default,
                AlwaysUnloadUnusedAssets = true, // Unload unused assets for performance
                ForceWaitFetch = false
            });
        }
    }
}
