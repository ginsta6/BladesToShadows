using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 start;
    public bool InPlace = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ReturnToPlace()
    {
        rectTransform.position = start;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        InPlace = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
        if (InPlace)
        {
            rectTransform.position = start;
            InPlace = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!InPlace)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            start = rectTransform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (!InPlace)
        {
            rectTransform.position = start;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!InPlace)
        {
            Debug.Log("OnDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

}
