using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour {
        
    [SerializeField] private GameObject Tablet;
    [SerializeField] private GameObject inventoryBar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    private SceneController sceneController;

    private bool isOn;
    // Use this for initialization
    void Start()
    {
        isOn = false;
        this.sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn == false && Input.GetKeyDown(KeyCode.I))
        {
            Tablet.SetActive(true);
            isOn = true;
            inventoryBar.SetActive(false);
            sceneController.freezeGame();
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Tablet.SetActive(false);
                isOn = false;
                inventoryBar.SetActive(true);
                sceneController.unfreezeGame();
            }
        }
    }
}
