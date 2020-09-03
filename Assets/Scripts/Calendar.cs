using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    public static Calendar instance;

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

    public Text information;
    private int limit = 27;      //Game end limit i.e. total days
    private static int currentday = -1;

    private void Start()
    {
        AddDay();

    }

    public void AddDay()
    {
        currentday++;
        NarcTimer.instance.Subtract();
        UpdateUI();

    }

    public void UpdateUI()
    {
        information.text = "Week: " + ((currentday / 7) + 1) + "\nDay: " + ((currentday % 7) + 1);
    }

    public bool CheckWeek()
    {
        if (currentday % 7 == 6)
            return true;
        else return false;
    }

    public bool CheckGameEnd()
    {
        if (currentday == limit)
            return true;
        else return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Debug.Log(currentday);
    }

    public int GetDayCount()
    {
        return currentday;
    }

    public void ResetDay()
    {
        currentday = -1;
    }

}
