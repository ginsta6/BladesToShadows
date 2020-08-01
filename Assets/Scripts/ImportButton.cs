using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportButton : MonoBehaviour
{
    [SerializeField] private Item item;

    public Transform slotsParent;
    private ImportInventory[] slots;

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<ImportInventory>();
    }

    public void AddToBuyList()
    {
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.UpdateUI();
                ImportContinueButton.instance.UpdateTotalAmount();
                break;
            }
            

        }
    }


}
