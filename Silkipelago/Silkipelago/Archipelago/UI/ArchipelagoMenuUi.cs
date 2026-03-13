using KaitoKid.ArchipelagoUtilities.Net.Client;
using Silkipelago.HarmonyPatches.NewGame;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;


namespace Silkipelago.Archipelago.UI
{
    public class ArchipelagoMenuUI
    {
        public bool shouldLaunchStartCutscene { get; set; }
        private Canvas _canvas;
        private bool _visible;
        private ILogger _logger;
        private ClickOnlyInputField _hostInput;
        private ClickOnlyInputField _portInput;
        private ClickOnlyInputField _slotInput;

        // ---------- Public API ----------

        public ArchipelagoMenuUI(ILogger logger)
        {
            _logger = logger;
        }

        public void InitUI()
        {
            if (_canvas != null)
                return;

            _canvas = ArchipelagoMenuBuilder.BuildUI(
                OnConnectClicked,
                Hide,
                out _hostInput,
                out _portInput,
                out _slotInput
            );
            ArchipelagoMenuBuilder.SetupTabNavigation(_hostInput, _portInput, _slotInput);


            _canvas.gameObject.SetActive(false);

            // sensible defaults
            _hostInput.text = "localhost";
            _portInput.text = "38281";
        }

        public void Toggle()
        {
            if (_canvas == null)
                InitUI();

            if (_visible)
                Hide();
            else
                Show();
        }

        public void Show()
        {
            if (_canvas == null)
                InitUI();

            _visible = true;
            _canvas.gameObject.SetActive(true);
            SelectionGuard.Instance?.EnableGuard();
            _hostInput.Select();
            _hostInput.ActivateInputField();
        }

        public void Hide()
        {
            SelectionGuard.Instance?.DisableGuard();
            if (_canvas == null)
                return;

            _visible = false;
            _canvas.gameObject.SetActive(false);
        }

        // ---------- Actions ----------

        private void OnConnectClicked()
        {
            _logger.LogInfo(
                $"Connect requested: {_hostInput.text}:{_portInput.text} ({_slotInput.text})"
            );

            if (!int.TryParse(_portInput.text, out var port))
            {
                _logger.LogError("Port must be a valid number");
                return;
            }

            var APConnectionInfo = new ArchipelagoConnectionInfo(_hostInput.text, port, _slotInput.text, false);
            var connected = ArchipelagoPlugin.App.UIContext.ConnectionHandler.ConnectToArchipelago(APConnectionInfo);
            if (connected)
            {
                Hide();
                if (shouldLaunchStartCutscene)
                {
                    UIStartNewGamePatch.SkipMenuNextCall();
                    UIManager.instance.StartNewGame();
                }
            }
        }
    }
}
