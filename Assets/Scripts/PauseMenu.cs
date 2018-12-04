using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private SceneController sceneController;
    [SerializeField]private GameObject pauseMenu;
    private bool isPaused;
    private GameObject OnGameUI;
    private GameObject DialogueCanvas;
    private GameObject Tablet;
    private GameObject PowerPanel;
    private GameObject DataPanel;
    private GameObject DisplayData;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
        this.sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        this.OnGameUI = GameObject.Find("OnGameUI");
        this.DialogueCanvas = GameObject.Find("DialogueCanvas");
        this.Tablet = GameObject.Find("Tablet");
        this.PowerPanel = GameObject.Find("PowerPanel");
        this.DataPanel = GameObject.Find("DataPanel");
        this.DisplayData = GameObject.Find("DisplayData");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pause();

        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resume();
                if (PowerPanel.activeInHierarchy == false || DataPanel.activeInHierarchy == false || DisplayData.activeInHierarchy == false)
            {
                sceneController.unfreezeGame();
            }
            }


        }
        
    }

    private void pause() {
        isPaused = true;
        sceneController.freezeGame();
        pauseMenu.SetActive(true);
        OnGameUI.SetActive(false);
        DialogueCanvas.SetActive(false);
        Tablet.SetActive(false);

    }

    public void resume() {
        isPaused = false;
        pauseMenu.SetActive(false);
        OnGameUI.SetActive(true);
        DialogueCanvas.SetActive(true);
        Tablet.SetActive(true);
    }
}
