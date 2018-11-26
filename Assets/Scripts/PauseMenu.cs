using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private SceneController sceneController;
    [SerializeField]private GameObject pauseMenu;
    // Use this for initialization
    void Start()
    {
        this.sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resume();

            }
        }
    }

    private void pause() {
        sceneController.freezeGame();
        pauseMenu.SetActive(true);

    }

    public void resume() {
        sceneController.unfreezeGame();
        pauseMenu.SetActive(false);
    }
}
