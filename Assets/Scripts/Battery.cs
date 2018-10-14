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
	private PowerSource powerSource;

	// Use this for initialization
	void Start () {
		inUse = false;
		powerSource = new PowerSource (max_power, power_index);
	}

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
		
	// Update is called once per frame
	void Update () {
		Debug.Log (this.powerSource.getPowerLevel ());
		if(powerSource.isEmpty())
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.red;
            this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.red;
        }
		else if (powerSource.isFull())
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
            this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
        }
	}




    public IEnumerator chargeBattery()
    {
        this.inUse = false;
        // Note: While block bellow used to look like this not entirley sure this is the correct changse 
		/*while(power_index <= max_power)
        {
            yield return new WaitForSeconds(1f);
            power_index = max_power;
        }*/

		while(powerSource.getPowerLevel() <= powerSource.getMaxPower())
		{
			yield return new WaitForSeconds(1f);
			//powerSource.givePower (powerSource.getMaxPower() - powerSource.getPowerLevel()); // power_index = max_power;
			power_index = max_power;
		}

        /*
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
        this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
        */
    }

	public PowerSource getPowerSource(){
		return powerSource; 
	}
}


