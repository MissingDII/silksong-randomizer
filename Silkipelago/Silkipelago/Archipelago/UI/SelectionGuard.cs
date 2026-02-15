using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionGuard : MonoBehaviour
{
    public static SelectionGuard Instance;

    private InputField _lockedField;
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
        _lockedField = null;
    }

    public void AllowSelection(InputField field)
    {
        if (!_enabledGuard) return;
        _lockedField = field;
    }

    void LateUpdate()
    {
        if (!_enabledGuard || _lockedField == null)
            return;

        var current = EventSystem.current.currentSelectedGameObject;

        if (current == _lockedField.gameObject)
            return;

        // restore focus WITHOUT triggering SelectAll loop
        EventSystem.current.SetSelectedGameObject(_lockedField.gameObject);

        if (!_lockedField.isFocused)
            _lockedField.ActivateInputField();

        // restore caret position instead of selecting all
        var caret = _lockedField.caretPosition;
        _lockedField.caretPosition = caret;
    }
}
