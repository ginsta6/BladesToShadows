using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class NewBehaviourScript : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;


}
