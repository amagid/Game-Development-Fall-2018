using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControlUP : MonoBehaviour {

    [SerializeField] private GameObject scene_controller;
    [SerializeField] private GameObject button_light;
    private PowerConsumer powerConsumer;
    private int current_level_num;

    // Use this for initialization
    void Start()
    {
        this.powerConsumer = this.getPowerConsumer();
        current_level_num = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bool deviceIsPowered = this.powerConsumer.powerDevice();
        if (deviceIsPowered)
        {
            if(current_level_num < 3)
            {
                current_level_num++;
                button_light.SetActive(true);
                scene_controller.GetComponent<SceneController>().StartCoroutine("loadLevel", current_level_num);
                button_light.SetActive(false);
            }
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
