using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    public int max_power = 100;

    public int power_index = 100;

    public bool inUse;

	private DoorController doorController;
	private SwitchController switchController;
	private CentralLightController centralLightController;

	public bool isInUse(){
		return this.inUse;
	}

	public void setIsInUse(bool inUse){
		this.inUse = inUse;
	}

	public void setSwitchController(SwitchController switchController){
		this.switchController = switchController;
	}

	public SwitchController getSwitchController(){
		return this.switchController;
	}

	public void setCentralLightController(CentralLightController centralLightController){
		this.centralLightController = centralLightController;
	}

	public CentralLightController getCentralLightController(){
		return this.centralLightController;
	}

	// used for determining which door it is opening
	public void setDoorController(DoorController doorController){
		this.doorController = doorController;
	}

	public DoorController getDoorController(){
		return this.doorController;
	}

	// Use this for initialization
	void Start () {
        inUse = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(isEmpty())
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.red;
            this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.red;
        }
        else if (isFull())
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
            this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
        }
	}

    public bool isEmpty()
    {
        return power_index <= 0;
    }

    public bool isFull()
    {
        return power_index >= 100;
    }

    public int getPowerIndex() {
        return this.power_index;
    }

    public IEnumerator useBattery() {
		setIsInUse (true);
        while (!isEmpty())
        {
            //battery will be empty in 5 seconds
            power_index -= 5;
            yield return new WaitForSeconds(0.2f);
        }
        /*
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.red;
        this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.red;
        */
		setIsInUse (false);
		if (switchController != null) {
			switchController.setBattery (null);
		} else {
			centralLightController.setBatteryOnSwitch (null);
		}
	}

    public IEnumerator chargeBattery()
    {
        inUse = false;
        while(power_index <= max_power)
        {
            yield return new WaitForSeconds(1f);
            power_index = max_power;
        }
        /*
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
        this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
        */
    }
}
