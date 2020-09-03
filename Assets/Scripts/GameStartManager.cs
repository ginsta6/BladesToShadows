using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public static int GameStart;

    // Start is called before the first frame update
    void Start()
    {
        if ((GameStart != 0) && (SceneManager.GetActiveScene().name == "Shop"))
        {
            Calendar.instance.ResetDay();
            ClientGenerator.instance.ResetSliders();
            ClientGenerator.instance.ChangeOdds(false);
            GameStart--;
            Debug.Log("Did something");
        }
        if ((GameStart != 0) && (SceneManager.GetActiveScene().name == "Supply"))
        {
            Coins.instance.ResetWalet();
            Inventory.instance.ClearInventory();
            GameStart--;
            Debug.Log("Did something");
        }
    }

}
