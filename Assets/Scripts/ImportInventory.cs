using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportInventory : MonoBehaviour
{
    public Image icon;
    public Text itemName;
    public Item item;


    public void RemoveItem()
    {
        icon.color = new Color(255,255,255,0);
        itemName.text = null;
        item = null;
        ImportContinueButton.instance.UpdateTotalAmount();
    }

    public void UpdateUI()
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.color = new Color(255, 255, 255, 255);
            itemName.text = item.itemName;
        }
        else icon.color = new Color(255, 255, 255, 0);
    }


}
