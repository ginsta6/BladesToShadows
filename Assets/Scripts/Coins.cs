using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static Coins instance;

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


    public static float balance = 500;
    public Text amount;
    public Image icon;

    public void UpdateUI()
    {
        amount.text = balance.ToString();
    }

    public void ItemSold(float price)
    {
        balance += price;
        FindObjectOfType<AudioManager>().Play("Sold");
        UpdateUI();
    }

    public void ItemBought(float price)
    {
        balance -= price;
        UpdateUI();
    }

    public float GetBalance()
    {
        return balance;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            balance += 500;
            UpdateUI();
        }
    }


}
