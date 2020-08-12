using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : MonoBehaviour
{
    public string NextSceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        Calendar.instance.ResetDay();
        FindObjectOfType<AudioManager>().Play("GameLoop");
        SceneManager.LoadScene("Supply");
    }

}
