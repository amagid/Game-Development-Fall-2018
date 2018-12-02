using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	public int max_power = 100;
	public int power_index = 100;
	private PowerSource powerSource;
	public PowerConsumer deviceBatteryIsAttachedTo = null; // WARNING: CURRENTLY ONLY SET UP FOR BATTERIES AND GENERIC SWITCH
    private Light light1;
    private Light light2;

	// Use this for initialization
	void Start () {
		powerSource = new PowerSource (max_power, power_index);
		powerSource.theBattery = this; // temporary bad code fix later
        Transform light1obj = this.gameObject.transform.Find("light1");
        Transform light2obj = this.gameObject.transform.Find("light2");
        if (light1obj != null)
        {
            this.light1 = light1obj.GetComponent<Light>();
        } else
        {
            this.light1 = null;
        }
        if (light2obj != null)
        {
            this.light2 = light2obj.GetComponent<Light>();
        } else
        {
            this.light2 = null;
        }

    }

	// Update is called once per frame
	void Update () {
		// Useful post to help show the rate at which batteries decrease or increase power
		//if(powerSource.getPowerLevel() != powerSource.getMaxPower()){
		//	Debug.Log ("this is the battery for the things " + powerSource.getPowerLevel());
		//}
		updateBatteryLights ();
	}


	/// <summary>
	/// Checks if battery is empty, if so sets the lights to red, checks if battery is full if so sets light to green.
	/// Will do nothing if battery is already the correct color.
	/// </summary>
	private void updateBatteryLights(){
		if(powerSource.isEmpty() && gameObject.GetComponent<Renderer>().material.color != Color.red)
		{
			this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (this.light1 != null)
                this.light1.color = Color.red;
            if (this.light2 != null)
                this.light2.color = Color.red;
            /*if (this.switchController != null) NOTE: Was in the previous doc will probably delete
			{
				this.switchController.deactivate();
			}*/
        }
		else if (!powerSource.isEmpty() && gameObject.GetComponent<Renderer>().material.color != Color.green)
		{
			this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (this.light1 != null)
    			this.light1.color = Color.green;
            if (this.light2 != null)
                this.light2.color = Color.green;
		}
	}

	public PowerSource getPowerSource(){
		return powerSource; 
	}

	public void setPowerSource(PowerSource ps)
	{
		this.powerSource = ps;
	}
}

