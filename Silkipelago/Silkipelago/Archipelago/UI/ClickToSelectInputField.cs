using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Silkipelago.Archipelago.UI
{
    public class ClickToSelectInputField :
        MonoBehaviour,
        IPointerDownHandler,
        IPointerClickHandler
    {
        private InputField _inputField;

        public void Setup(InputField inputField)
        {
            _inputField = inputField;
        }

        // Prevent InputField from auto-activating itself
        public void OnPointerDown(PointerEventData eventData)
        {
            eventData.Use(); // stops InputField default handler
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            _inputField.ActivateInputField();

            SelectionGuard.Instance.AllowSelection(_inputField.gameObject);
        }
    }
}
