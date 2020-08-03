using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenotorySlot : MonoBehaviour
{
    public Image image;
    public Item item;


    private void Update()
    {
        if ((item != null) && (image.sprite == null))
        {
            UpdateSlot();
        }
    }

    public void SellItem()
    {
        if (item != null)
        {
            Inventory.instance.RemoveItem(item);
            Coins.instance.ItemSold(item.price * 1.3f);
            DeleteSlotItem();

        }
    }

    public void BuyItem()
    {
        if ((item != null) && (Inventory.instance.GetCount() < 7))
        {
            if (Coins.instance.GetBalance() >= item.price)
            {
                Inventory.instance.AddItem(item);
                Coins.instance.ItemBought(item.price);
                DeleteSlotItem();
            }
        }
    }

    public void DeleteSlotItem()
    {
        item = null;
        image.sprite = null;
        image.color = new Color(0, 0, 0, 0);
    }

    public void AddItem(Item _item)
    {
        item = _item;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        image.sprite = item.image;
        image.color = new Color(255, 255, 255, 255);
        //text
    }


}
