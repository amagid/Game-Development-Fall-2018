using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour, PoweredOperation
{
    private bool deviceActive;
    private PowerConsumer powerConsumer;

    //TODO: Rework this to not just copy/paste the NoteController code
    [SerializeField] Sprite image;
    // the Content child object of ScrollView
    [SerializeField] GameObject content;
    // the DisplayData object of OnGameGUI
    [SerializeField] GameObject displayContent;
    // the player game object
    [SerializeField] GameObject player;
    // the SecondaryCamera game object
    [SerializeField] GameObject secondaryCameraGO;

    void Start()
    {
        deviceActive = false;
        this.powerConsumer = this.getPowerConsumer();
        displayContent.SetActive(false);
        secondaryCameraGO.SetActive(false);
        Image img = content.GetComponent<Image>();
        img.sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.timeScale == 0)
			return;
        // Activate/Operate/Deactivate device based on powerConsumer state
        bool deviceIsPowered = this.powerConsumer.powerDevice();
        if (deviceIsPowered && !this.isActive())
        {
            this.activate();
        }
        else if (deviceIsPowered && this.isActive())
        {
            this.operate();
        }
        else if (!deviceIsPowered && this.isActive())
        {
            this.deactivate();
        }
    }

    public void activate()
    {
        this.deviceActive = true;
        // display panel here w/ note
        freezeGame();
        Image img = content.GetComponent<Image>();
        img.sprite = image;
        displayContent.SetActive(true);
    }

    // removes the player from the game temporarily so sanity/power doesn't decrease
    void freezeGame()
    {
        GameObject camera = player.transform.Find("Camera").gameObject;
        secondaryCameraGO.transform.position = camera.transform.position;
        secondaryCameraGO.transform.rotation = camera.transform.rotation;
        secondaryCameraGO.transform.localEulerAngles = camera.transform.localEulerAngles;
        player.SetActive(false);
        secondaryCameraGO.SetActive(true);
    }

    void unfreezeGame()
    {
        secondaryCameraGO.SetActive(false);
        player.SetActive(true);
    }

    // Closes the content display panel
    public void closePanel()
    {
        displayContent.SetActive(false);
        unfreezeGame();
    }

    public void operate() {}

    public void deactivate()
    {
        deviceActive = false;
    }

    public bool isActive()
    {
        return deviceActive;
    }

    public PowerConsumer getPowerConsumer()
    {
        PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
        if (pc == null)
        {
            throw new NoPowerConsumerException("ComputerControllers must always have PowerConsumers! Please attach a PowerConsumer component in the Unity editor.");
        }
        return pc;
    }
}
