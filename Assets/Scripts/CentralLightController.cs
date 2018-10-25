using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralLightController : MonoBehaviour {

    private Inventory inventory;

    //Turn this into an array of lights.

    [SerializeField] private GameObject central_lights;

    private bool atSwitch = false;

    public double xOffset;

    public double yOffset;

    public double zOffset;

    private GameObject batteryOnSwitch = null;

    private GameObject GreenLights;
    private GameObject RedLights;

    public GameObject getBatteryOnSwitch(){
		return this.batteryOnSwitch;
	}

	public void setBatteryOnSwitch(GameObject batteryOnSwitch){
		this.batteryOnSwitch = batteryOnSwitch;
	}

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        Transform gl = this.gameObject.transform.Find("GreenLights");
        Transform rl = this.gameObject.transform.Find("RedLights");
        if (gl != null)
        {
            this.GreenLights = gl.gameObject;
        }
        if (rl != null)
        {
            this.RedLights = rl.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (atSwitch)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventory.batteryCount() >= 1 && batteryOnSwitch == null)
            {
                turnOnLights();
            }
        }
		if (batteryOnSwitch != null && batteryOnSwitch.GetComponent<Battery>().getPowerSource().isEmpty())
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
        GameObject battery = inventory.getFirstBattery();
        batteryOnSwitch = battery;
        battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
        battery.SetActive(true);
		if (battery.GetComponent<Battery>().getPowerSource().isEmpty())
        {
            Debug.Log("Battery is empty!");
            return;
        }
        central_lights.SetActive(true);
		battery.GetComponent<Battery> ().setCentralLightController (this);
        battery.GetComponent<Battery>().StartCoroutine("useBattery");
        if (this.GreenLights != null && !this.GreenLights.activeInHierarchy)
        {
            this.GreenLights.SetActive(true);
        }
        if (this.RedLights != null && this.RedLights.activeInHierarchy)
        {
            this.RedLights.SetActive(false);
        }
    }

    void turnOffLights()
    {
        central_lights.SetActive(false);
        if (this.GreenLights != null && this.GreenLights.activeInHierarchy)
        {
            this.GreenLights.SetActive(false);
        }
        if (this.RedLights != null && !this.RedLights.activeInHierarchy)
        {
            this.RedLights.SetActive(true);
        }
    }
}
