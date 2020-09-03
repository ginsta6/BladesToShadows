using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarcTimer : MonoBehaviour
{
    public static NarcTimer instance;

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

    public static int timer;
    public Text display;
    public GameObject textHolder;

    public void SetTimer()
    {
        timer = 15;
        textHolder.SetActive(true);
    }

    public void Subtract()
    {
        timer--;
        UpdateUI();
        if (timer == 0)
        {
            textHolder.SetActive(false);
            ClientGenerator.instance.ChangeOdds(false);
        }
    }

    private void Update()   //constantly updating for no reason
    {
        if (timer > 0)
        {
            textHolder.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        display.text = timer.ToString();
    }
}
