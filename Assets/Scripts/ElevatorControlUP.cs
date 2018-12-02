using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControlUP : MonoBehaviour, DirectOperation {

    [SerializeField] private GameObject scene_controller;
    [SerializeField] private GameObject button_light;
    private PowerConsumer powerConsumer;
    private int current_level_num;
    private bool isCurrentlyMoving;
    private int numBatteries = 0;
	private MachineController theReactor;

    // Use this for initialization
    void Start()
    {
		theReactor = GameObject.Find ("Reactor").GetComponent<MachineController>();
        this.powerConsumer = this.getPowerConsumer();
        current_level_num = scene_controller.GetComponent<SceneController>().current_level_num;
        isCurrentlyMoving = scene_controller.GetComponent<SceneController>().isElevatorMoving;
    }

    // Update is called once per frame
    void Update()
    {
        current_level_num = scene_controller.GetComponent<SceneController>().current_level_num;
        isCurrentlyMoving = scene_controller.GetComponent<SceneController>().isElevatorMoving;
		if (!isCurrentlyMoving && theReactor.getIsActivated() && current_level_num < 3 && this.powerConsumer.powerDevice()) // BOOLEAN IS HERE <----------------------------------------------------------------------
        {
            StartCoroutine("loadUpperLevel");
        }
    }

    private IEnumerator loadUpperLevel()
    {
        scene_controller.GetComponent<SceneController>().button_light = this.button_light;
        scene_controller.GetComponent<SceneController>().StartCoroutine("loadLevel", current_level_num + 1);
        yield return new WaitForSeconds(1f);
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

    // DirectOperation classes used for restricting elevator on first level
    public void activate()
    {
        this.numBatteries++;
    }
    public void operate()
    {
    }
    public void deactivate()
    {
        this.numBatteries--;
    }
    public bool isActive()
    {
        return this.numBatteries == 3;
    }
}
