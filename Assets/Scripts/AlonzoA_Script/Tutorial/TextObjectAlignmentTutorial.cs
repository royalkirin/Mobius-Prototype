using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObjectAlignmentTutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text _mainText;

    [Header("Transforms")]
    [SerializeField] RectTransform _origTransform;
    [SerializeField] RectTransform _newTransform;

    public void NewPosition()
    {
        _mainText.rectTransform.position = _newTransform.position;
    }

    public void OriginalPos()
    {
        _mainText.rectTransform.position = _origTransform.position;
    }
}
