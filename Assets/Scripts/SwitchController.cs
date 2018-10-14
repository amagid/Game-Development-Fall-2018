using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comment for Git Testing, feel free to delete.

// Note: Powered operation interface 
public class SwitchController : MonoBehaviour, PoweredOperation {

	private Inventory inventory;

	[SerializeField] private GameObject switchDevice;
	private DirectOperation deviceController; 
	private bool atSwitch = false;
    public double xOffset;
    public double yOffset;
    public double zOffset;
	private bool active = false;
	// the battery that is attached to the switch
	private Battery battery = null;

	private PowerConsumer powerConsumer; 




	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
		this.powerConsumer = this.getPowerConsumer();
		this.deviceController = this.switchDevice.GetComponent<DirectOperation> ();
	}
		

	// Update is called once per frame
	void Update () {
		// sets the battery in the switc
		if (atSwitch) {
			if (battery != null) {
				Debug.Log (battery.name + " " + battery.getPowerSource().getPowerLevel());
			}
			if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 1 && battery == null)
			{
				setBattery ();
				battery.setIsInUse (true); //NOTE: temporary battery is in use will be deleted soon
			}
		}

		// Activate/Operate/Deactivate device based on powerConsumer state
		// calls the Activate/Operat/Deactivate of the device the switch powers
		bool deviceIsPowered = this.powerConsumer.powerDevice();
		if (deviceIsPowered && !this.isActive())
		{
			this.activate();
		} else if (deviceIsPowered && this.isActive())
		{
			this.operate();
		} else if (!deviceIsPowered && this.isActive())
		{
			//battery.setIsInUse (false); //NOTE: temporary battery is in use will be deleted soon
			this.deactivate();
		}
	}


	public void setBattery(){ // power consumer attach power source
		// Sets in game battery object on to the switch
		GameObject batteryGO = inventory.getFirstItem();
		Debug.Log ("BATTERY NAME: " + batteryGO.name);
		batteryGO.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
		batteryGO.SetActive(true);

		// if there are no batteries with power
		if (batteryGO.GetComponent<Battery> ().getPowerSource ().isEmpty ()) {
			Debug.Log ("Battery is empty!");
		} else {
			powerConsumer.attachPowerSource(batteryGO.GetComponent<Battery>().getPowerSource());
		}
	}

	public void setBattery(Battery battery){ // power consumer attach power source
		this.battery = battery;
	}
		

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atSwitch = true;
	}

	void OnTriggerExit(Collider other) {
		atSwitch = false;
	}


	/// <summary>
	/// Perform initial activation work for this device upon recieving sufficient Power
	/// </summary>
	public void activate(){
		this.active = true;
		this.deviceController.activate ();
	}


	/// <summary>
	/// Tick-by-tick operation of this device. Operate() should finish in one Update() call.
	/// </summary>
	public void operate(){
		deviceController.operate ();
	}
		
	/// <summary>
	/// Perform deactivation work for this device upon losing sufficient Power
	/// </summary>
	public void deactivate(){
		Debug.Log ("HELLO");

		this.active = false;
		deviceController.deactivate ();
	}
		
	public bool isActive(){
		return active;
	}

	// Coppied from LightSourceController maybe should go in a parent class 
	public PowerConsumer getPowerConsumer()
	{
		PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
		if (pc == null)
		{
			throw new NoPowerConsumerException("SwitchController must always have PowerConsumers! Please attach a PowerConsumer component in the Unity editor.");
		}
		return pc;
	}
}
