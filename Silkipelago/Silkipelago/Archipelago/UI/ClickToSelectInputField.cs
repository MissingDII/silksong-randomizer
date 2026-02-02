using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Silkipelago.Archipelago.UI
{
    public class ClickToSelectInputField : MonoBehaviour, IPointerClickHandler
    {
        private InputField _inputField;

        public void Setup(InputField inputField)
        {
            _inputField = inputField;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            _inputField.ActivateInputField();

            // Allow Selection
            SelectionGuard.Instance?.AllowSelection(_inputField);
        }
    }
}
