using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

    public void PlayGame()
    {

        SceneManager.LoadScene("TestScene1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
