using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoorSwitch : MonoBehaviour {

    private bool deviceActive;
    private PowerConsumer powerConsumer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject maze_controller;


    // Use this for initialization
    void Start () {
        deviceActive = false;
        this.powerConsumer = this.getPowerConsumer();
    }
	
	// Update is called once per frame
	void Update () {
        bool deviceIsPowered = this.powerConsumer.powerDevice();
        if (deviceIsPowered)
        {
            maze_controller.GetComponent<MazeController>().nextMazeSequence();
        }
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
