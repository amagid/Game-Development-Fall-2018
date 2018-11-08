using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControlUP : MonoBehaviour {

    [SerializeField] private GameObject scene_controller;
    [SerializeField] private GameObject button_light;
    private PowerConsumer powerConsumer;
    private int current_level_num;
    private bool isCurrentlyMoving;

    // Use this for initialization
    void Start()
    {
        this.powerConsumer = this.getPowerConsumer();
        current_level_num = 1;
        isCurrentlyMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(button_light.active);
        bool deviceIsPowered = this.powerConsumer.powerDevice();
        if (deviceIsPowered && !isCurrentlyMoving)
        {
            if(current_level_num < 3)
            {
                StartCoroutine("loadUpperLevel");
            }
        }
    }

    private IEnumerator loadUpperLevel()
    {
        isCurrentlyMoving = true;
        current_level_num++;
        scene_controller.GetComponent<SceneController>().button_light = this.button_light;
        scene_controller.GetComponent<SceneController>().StartCoroutine("loadLevel", current_level_num);
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
}
