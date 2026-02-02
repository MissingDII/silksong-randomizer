using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldSelectionFix :
    MonoBehaviour,
    ISelectHandler
{
    private InputField _field;

    void Awake()
    {
        _field = GetComponent<InputField>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(CollapseNextFrame());
    }

    private IEnumerator CollapseNextFrame()
    {
        yield return null;

        if (_field == null) yield break;

        int end = _field.text != null ? _field.text.Length : 0;

        _field.caretPosition = end;
        _field.selectionAnchorPosition = end;
        _field.selectionFocusPosition = end;

        _field.ForceLabelUpdate();
    }
}
