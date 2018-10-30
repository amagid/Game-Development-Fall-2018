using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	public int max_power = 100;
	public int power_index = 100;
	private PowerSource powerSource;
	public PowerConsumer deviceBatteryIsAttachedTo = null; // WARNING: CURRENTLY ONLY SET UP FOR BATTERIES AND GENERIC SWITCH

	// Use this for initialization
	void Start () {
		powerSource = new PowerSource (max_power, power_index);
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
			this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.red;
			this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.red;
			/*if (this.switchController != null) NOTE: Was in the previous doc will probably delete
			{
				this.switchController.deactivate();
			}*/
		}
		else if (powerSource.isFull() && gameObject.GetComponent<Renderer>().material.color != Color.green)
		{
			this.gameObject.GetComponent<Renderer>().material.color = Color.green;
			this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
			this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
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

