using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreBoard : MonoBehaviour
{
    public Transform slotsParent;
    private HighscoreSlot[] slots;

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<HighscoreSlot>();

        for (int i = 0; i < SaveManager.instance.activeSave.data.Count; i++)
        {
            slots[i].UpdateSlot(SaveManager.instance.activeSave.data[i].Gold, SaveManager.instance.activeSave.data[i].dayCount);
        }
    }
}
