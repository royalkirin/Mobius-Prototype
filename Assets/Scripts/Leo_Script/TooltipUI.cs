using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {

    public static TooltipUI Instance { get; private set; }



    [SerializeField] private RectTransform canvasRectTransform;

    private TooltipCaller tooltipCaller;
    private Image toolTipSprite;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private float tooltipTimer;

    private void Awake() {
        Instance = this;

        tooltipCaller = this.transform.parent.GetComponent<TooltipCaller>();
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        toolTipSprite = transform.Find("toolTipSprite").GetComponent<Image>();

        Hide();
    }

    private void Update() {
        HandleFollowMouse();

        if (tooltipTimer != 0) {
            tooltipTimer -= Time.deltaTime;
            if (tooltipTimer <= 0) {
                Hide();
            }
        }
    }

    private void HandleFollowMouse() {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText) {
        textMeshPro.gameObject.SetActive(true);
        backgroundRectTransform.gameObject.SetActive(true);

        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText = null, Card _card = null, float _tooltipTimer = 0) {
        this.tooltipTimer = _tooltipTimer;
        gameObject.SetActive(true);

        if (_card != null) {
            toolTipSprite.gameObject.SetActive(true);
            toolTipSprite.sprite = _card.GetFrontImage();
        }

        if (tooltipText != null) {
            textMeshPro.gameObject.SetActive(true);
            backgroundRectTransform.gameObject.SetActive(true);
            SetText(tooltipText);
        }

        HandleFollowMouse();
    }

    public void Hide() {

        toolTipSprite.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        backgroundRectTransform.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    public void StopShowingUI() {
        tooltipCaller.StopShowingUI();
    }

    public void ResumeShowingUI() {
        tooltipCaller.ResumeShowingUI();
    }


}
