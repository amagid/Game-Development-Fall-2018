using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralLightController : MonoBehaviour {

    private Inventory inventory;

    [SerializeField] private GameObject central_lights;

    private bool atSwitch = false;

    public double xOffset;

    public double yOffset;

    public double zOffset;

    private GameObject batteryOnSwitch = null;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atSwitch)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 1 && batteryOnSwitch == null)
            {
                turnOnLights();
            }
        }
        if (batteryOnSwitch != null && batteryOnSwitch.GetComponent<Battery>().isEmpty())
        {
            turnOffLights();
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

    void turnOnLights()
    {
        GameObject battery = inventory.getFirstItem();
        batteryOnSwitch = battery;
        battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
        battery.SetActive(true);
        if (battery.GetComponent<Battery>().isEmpty())
        {
            Debug.Log("Battery is empty!");
            return;
        }
        central_lights.SetActive(true);
        battery.GetComponent<Battery>().StartCoroutine("useBattery");
    }

    void turnOffLights()
    {
        central_lights.SetActive(false);
    }
}
