using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImportContinueButton : MonoBehaviour
{
    public static ImportContinueButton instance;

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

    public Transform itemParent;
    private ImportInventory[] buyList;
    public Text totalAmount;
    private int totalPrice = 0;

    private void Start()
    {
        buyList = itemParent.GetComponentsInChildren<ImportInventory>();
    }

    public void UpdateTotalAmount()
    {
        totalPrice = 0;
        totalAmount.text = "0 g.";
        foreach (var item in buyList)
        {
            if (item.item != null)
            {
                totalPrice += item.item.price;
                totalAmount.text = totalPrice.ToString() + " g.";
            }
        }
    }

    public void Continue()
    {
        if (!CheckSpace())
        {
            //show error
            Debug.Log("Not enought space in inventory");
            return;
        }
        if (!(Coins.instance.GetBalance() >= totalPrice))
        {
            //show error
            Debug.Log("Not enough money");
            return;
        }
        AddToInventory();
        Coins.instance.ItemBought(totalPrice);
        ChangeScene();
        
    }

    public bool CheckSpace()
    {
        int needed = 0;
        foreach (var item in buyList)
        {
            if (item.item != null)
                needed++;
        }

        if (7 - Inventory.instance.GetCount() >= needed)
        {
            return true;
        }
        else return false;
    }

    private void AddToInventory()
    {
        foreach (var item in buyList)
        {
            if (item.item != null)
                Inventory.instance.AddItem(item.item);
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Shop");
    }

}
