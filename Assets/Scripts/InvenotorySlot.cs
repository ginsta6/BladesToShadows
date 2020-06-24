using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenotorySlot : MonoBehaviour
{
    public Image image;


    public void SellItem()
    {
        if (image != null)
        {
            image.sprite = null;
            image.color = new Color(0,0,0,0);
            Coins.instance.ItemSold(50);
            Coins.instance.UpdateUI();
        }
    }
}
