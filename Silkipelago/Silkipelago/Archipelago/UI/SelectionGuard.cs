using UnityEngine;
using UnityEngine.EventSystems;

namespace Silkipelago.Archipelago.UI
{
    public class SelectionGuard : MonoBehaviour
    {
        public static SelectionGuard Instance;

        private GameObject _lastAllowedSelection;
        private bool _enabledGuard;

        void Awake()
        {
            Instance = this;
        }

        public void EnableGuard()
        {
            _enabledGuard = true;
        }

        public void DisableGuard()
        {
            _enabledGuard = false;
            _lastAllowedSelection = null;
        }

        public void AllowSelection(GameObject go)
        {
            if (!_enabledGuard) return;
            _lastAllowedSelection = go;
        }

        void LateUpdate()
        {
            if (!_enabledGuard) return;

            var current = EventSystem.current.currentSelectedGameObject;

            if (current != null && current != _lastAllowedSelection)
            {
                EventSystem.current.SetSelectedGameObject(_lastAllowedSelection);
            }
        }
    }
}
