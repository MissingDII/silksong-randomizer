namespace Silkipelago.Archipelago.UI
{
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ClickOnlyInputField : InputField
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            // Do NOT call base → prevents auto-activation
        }

    }

}
