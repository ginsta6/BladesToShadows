using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Updater : MonoBehaviour
{
    private Scene currentScene;


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() != currentScene)
        {
            Debug.Log("Scene changed");//cia neveikia
            UpdateEverything();
            currentScene = SceneManager.GetActiveScene();
        }
    }

    void UpdateEverything()
    {
        Coins.instance.UpdateUI();
        if (SceneManager.GetActiveScene().name == "Shop")
            Inventory.instance.UpdateSlots();
    }
}
