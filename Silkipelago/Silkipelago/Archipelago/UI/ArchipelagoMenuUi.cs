using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using Silkipelago.HarmonyPatches;
using Silkipelago.HarmonyPatches.NewGame;
using Silkipelago.Items;
using System;
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
            ConnectToArchipelago(() => InitializeAfterConnection());
        }

        private static void InitializeAfterConnection()
        {
            var locationChecker = SilksongLocationChecker.Instance;
            var itemManager = SilksongItemManager.Instance;
            var archipelago = SilksongArchipelagoClient.Instance;

            locationChecker.VerifyNewLocationChecksWithArchipelago();
            locationChecker.SendAllLocationChecks();
            var patchInitializer = new PatchInitializer();
            patchInitializer.InitializeConnectedPatches(_logger, _harmony, _archipelagoClient, _silksongLocationChecker);
            Hide();
            _archipelagoClient._shouldDoInitialLoad = true;
            StartNewGamePatch.SkipMenuNextCall();
            UIManager.instance.StartNewGame();
        }

        private static void ConnectToArchipelago(Action actionAfterConnection)
        {
            var archipelago = SilksongArchipelagoClient.Instance;

            if (APConnectionInfo == null)
            {
                _logger.LogMessage($"Tried to connect, but no information provided!");
                return;
            }

            if (archipelago.IsConnected)
            {
                _logger.LogMessage($"Tried to connect, but already connected!");
                return;
            }

            var connectionResult = archipelago.ConnectToMultiworld(APConnectionInfo);
            if (!connectionResult.Success || !archipelago.IsConnected)
            {
                APConnectionInfo = null;
                var userMessage =
                    $"Could not connect to archipelago.{Environment.NewLine}Message: {connectionResult.Message}{Environment.NewLine}Please verify the connection info and that the server is available.{Environment.NewLine}";
                _logger.LogError(userMessage);
                return;
            }

            _logger.LogMessage($"Connected to Archipelago as {archipelago.SlotData.SlotName}.");
            actionAfterConnection?.Invoke();
        }
    }
}
