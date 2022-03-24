using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxAlignmentTutorial : MonoBehaviour
{
    [SerializeField] Image _textBox;
    [Header("Transforms")]
    [SerializeField] RectTransform _origTransform;
    [SerializeField] RectTransform _newTransform;

    public void NewPosition()
    {
        _textBox.rectTransform.position = _newTransform.position;
    }

    public void OriginalPos()
    {
        _textBox.rectTransform.position = _origTransform.position;
    }
}
