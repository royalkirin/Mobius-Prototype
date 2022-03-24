using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageCount : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text _pageCounterReference;

    [Header("Values")]
    [SerializeField] int curPageNumb = 0;
    [SerializeField] int maxPageNumb = 12;

    private void Start()
    {
        curPageNumb = 0;
        IncreasePageNumb();
    }

    public void IncreasePageNumb()
    {
        curPageNumb++;
        UpdatePageCount();
    }

    private void UpdatePageCount()
    {
        _pageCounterReference.text = curPageNumb + "/" + maxPageNumb;
    }
}
