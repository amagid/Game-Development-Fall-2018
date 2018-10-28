using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGenerator : MonoBehaviour {

    private Inventory inventory;

    [SerializeField] private GameObject elevator_door;

    [SerializeField] private GameObject scene_controller;

    private bool atSwitch = false;

    public double xOffset;

    public double yOffset;

    public double zOffset;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atSwitch)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 3)
            {
                useBattery();
                elevator_door.GetComponent<DoorController>().StartCoroutine("closeDoor");
                scene_controller.GetComponent<SceneController>().lvl1_complete = true;
                scene_controller.GetComponent<SceneController>().StartCoroutine("loadLevel2");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
            atSwitch = true;
    }

    void OnTriggerExit(Collider other)
    {
        atSwitch = false;
    }

    void useBattery()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject battery = inventory.getFirstItem();
            battery.transform.position = transform.position - new Vector3(0f,0f,1f) + new Vector3((float)xOffset, 0f, (float)(i * zOffset));
            battery.SetActive(true);
			if (battery.GetComponent<Battery>().getPowerSource().isEmpty())
            {
                battery.GetComponent<Battery>().StartCoroutine("chargeBattery");
            }
        }
    }
}
