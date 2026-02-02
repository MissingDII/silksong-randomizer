using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Silkipelago.Archipelago.UI
{
    public class TabNavigationHandler : MonoBehaviour, IUpdateSelectedHandler
    {
        private List<ClickOnlyInputField> _clickOnlyInputFields;
        private int _currentIndex;

        public void Setup(List<ClickOnlyInputField> ClickOnlyInputFields, int currentIndex)
        {
            _clickOnlyInputFields = ClickOnlyInputFields;
            _currentIndex = currentIndex;
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                int nextIndex;
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // Shift+Tab: go to previous
                    nextIndex = (_currentIndex - 1 + _clickOnlyInputFields.Count) % _clickOnlyInputFields.Count;
                }
                else
                {
                    // Tab: go to next
                    nextIndex = (_currentIndex + 1) % _clickOnlyInputFields.Count;
                }

                var nextField = _clickOnlyInputFields[nextIndex];

                EventSystem.current.SetSelectedGameObject(nextField.gameObject);
                nextField.ActivateInputField();

                SelectionGuard.Instance?.AllowSelection(nextField);

            }
        }
    }
}
