using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public static List<Item> inventory = new List<Item>();

    public Transform slotsParent;
    private InvenotorySlot[] slots;

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InvenotorySlot>();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }

    public int GetCount()
    {
        return inventory.Count;
    }

    public void UpdateSlots()
    {
        ClearSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
            {
                slots[i].AddItem(inventory[i]);
            }


        }

    }

    public void ClearSlots()
    {
        for (int i = 0; i < 7; i++)
            slots[i].DeleteSlotItem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var item in inventory)
            {
                Debug.Log("Item: " + item.itemName);
            }
        }
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }

}
