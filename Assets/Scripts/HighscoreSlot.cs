using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreSlot : MonoBehaviour
{
    public Text gold;
    public Text weeks;
    public Text days;

    public void UpdateSlot(float g, int totalDays)
    {
        gold.text = g.ToString() + "g.";
        weeks.text = "Week: " + ((totalDays / 7) + 1);
        days.text = "Day: " + ((totalDays % 7) + 1);
    }
}
