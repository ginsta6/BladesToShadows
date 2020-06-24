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


    public static int balance = 0;
    public Text amount;
    public Image icon;

    public void UpdateUI()
    {
        amount.text = balance.ToString();
    }

    public void ItemSold(int price)
    {
        balance += price;
    }



}
