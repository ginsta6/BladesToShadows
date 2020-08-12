using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{
    public static bool InPlace = false;
    public Image image;
    public Item InSlot;
    public GameObject temp;
    private Sprite previous;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            temp = eventData.pointerDrag;
            previous = image.sprite;
            image.sprite = eventData.pointerDrag.GetComponent<InvenotorySlot>().item.image;
            InSlot = eventData.pointerDrag.GetComponent<InvenotorySlot>().item;
            Debug.Log(eventData.pointerDrag.GetComponent<InvenotorySlot>().item.itemName);
            eventData.pointerDrag.SetActive(false);
            // eventData.pointerDrag.GetComponent<DragDrop>().InPlace = true;
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

            
        }
    }

    public void OnClickReturn()
    {
        if (InSlot != null)
        {
            image.sprite = previous;
            temp.SetActive(true);
            temp.GetComponent<DragDrop>().ReturnToPlace();
            Debug.Log(temp.GetComponent<InvenotorySlot>().item.itemName);
            InSlot = null;
            temp = null;
        }
    }
}
