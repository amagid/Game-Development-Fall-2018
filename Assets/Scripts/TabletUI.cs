using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour {
        
    [SerializeField] private GameObject Tablet;
    [SerializeField] private GameObject inventoryBar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    private bool isOn;
    // Use this for initialization
    void Start()
    {
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn == false && Input.GetKeyDown(KeyCode.I))
        {
            Tablet.SetActive(true);
            isOn = true;
            inventoryBar.SetActive(false);
            Pause();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Tablet.SetActive(false);
                isOn = false;
                inventoryBar.SetActive(true);
                Resume();

            }
        }
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        player.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<PowerConsumer>().enabled = true;
        player.GetComponent<PlayerCharacter>().enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<PlayerCharacter>().enabled = false;
        player.GetComponent<PowerConsumer>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
    }
}
