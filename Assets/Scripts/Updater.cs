using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Updater : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField]
    private GameObject inv;


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() != currentScene)
        {
            Debug.Log("Scene changed");
            UpdateEverything();
            currentScene = SceneManager.GetActiveScene();
        }

    }

    void UpdateEverything()
    {
        Coins.instance.UpdateUI();
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            inv.SetActive(true);
            Inventory.instance.UpdateSlots();
        }
        else if (SceneManager.GetActiveScene().name == "Supply")
            inv.SetActive(false);
    }
}
