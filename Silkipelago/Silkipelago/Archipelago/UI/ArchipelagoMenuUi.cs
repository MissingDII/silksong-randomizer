using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using Silkipelago.HarmonyPatches.NewGame;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;


namespace Silkipelago.Archipelago.UI
{
    public static class ArchipelagoMenuUI
    {
        private static Canvas _canvas;
        private static bool _visible;
        private static ILogger _logger;
        private static Harmony _harmony;
        private static SilksongArchipelagoClient _archipelagoClient;
        private static SilksongLocationChecker _silksongLocationChecker;
        private static ClickOnlyInputField _hostInput;
        private static ClickOnlyInputField _portInput;
        private static ClickOnlyInputField _slotInput;
        private static ArchipelagoConnectionInfo APConnectionInfo { get; set; }

        // ---------- Public API ----------

        public static void Init(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            if (_canvas != null)
                return; // already initialized
            _logger = logger;
            _harmony = harmony;
            _archipelagoClient = archipelagoClient;
            _silksongLocationChecker = silksongLocationChecker;
            InitUI();
        }

        public static void InitUI()
        {
            if (_canvas != null)
                return;

            _canvas = ArchipelagoMenuBuilder.BuildUI(
                OnConnectClicked,
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

        public static void Toggle()
        {
            if (_canvas == null)
                InitUI();

            if (_visible)
                Hide();
            else
                Show();
        }

        public static void Show()
        {
            if (_canvas == null)
                InitUI();

            _visible = true;
            _canvas.gameObject.SetActive(true);
            SelectionGuard.Instance?.EnableGuard();
            _hostInput.Select();
            _hostInput.ActivateInputField();
        }

        public static void Hide()
        {
            SelectionGuard.Instance?.DisableGuard();
            if (_canvas == null)
                return;

            _visible = false;
            _canvas.gameObject.SetActive(false);
        }

        // ---------- Actions ----------

        private static void OnConnectClicked()
        {
            _logger.LogInfo(
                $"Connect requested: {_hostInput.text}:{_portInput.text} ({_slotInput.text})"
            );

            if (!int.TryParse(_portInput.text, out var port))
            {
                _logger.LogError("Port must be a valid number");
                return;
            }

            APConnectionInfo = new ArchipelagoConnectionInfo(_hostInput.text, port, _slotInput.text, false);
            var connected = ArchipelagoConnectionHandler.ConnectToArchipelago(APConnectionInfo);
            if (connected)
            {
                Hide();
                UIStartNewGamePatch.SkipMenuNextCall();
                UIManager.instance.StartNewGame();
            }
        }
    }
}
