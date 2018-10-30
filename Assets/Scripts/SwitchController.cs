using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comment for Git Testing, feel free to delete.

// Note: Powered operation interface 
public class SwitchController : MonoBehaviour, PoweredOperation {

	private Inventory inventory;

	[SerializeField] private GameObject switchDevice;
	private DirectOperation deviceController; 
<<<<<<< HEAD
	public double xOffset;
	public double yOffset;
	public double zOffset;
	private GameObject GreenLights; // new
	private GameObject RedLights; // new
	private bool active = false;
	// the battery that is attached to the switch
	private Battery battery = null;
	private PowerConsumer powerConsumer;
=======
    public double xOffset;
    public double yOffset;
    public double zOffset;
	private bool active = false;
	// the battery that is attached to the switch
	private Battery battery = null;

	private PowerConsumer powerConsumer; // just public for the time being should be switched
>>>>>>> ede83ecdeb9a6a452e419e3821774de75aa84cff


	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
		this.powerConsumer = this.getPowerConsumer();
		this.deviceController = this.switchDevice.GetComponent<DirectOperation> ();
<<<<<<< HEAD


		// NEW --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
		// NEW --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




	}

=======
	}
		
>>>>>>> ede83ecdeb9a6a452e419e3821774de75aa84cff

	// Update is called once per frame
	void Update () {
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
			this.deactivate();
		}
	}
		
	// called from playerCharacter when Input.getkeydown KeyCode.E
	// sets the battery object active and attaches it to the power source 
	public void setBattery(GameObject battery){ // power consumer attach power source
		//this.battery = battery;
		// Sets in game battery object on to the switch
		Debug.Log ("BATTERY NAME: " + battery.name);
		battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
		battery.SetActive(true);
		battery.GetComponent<Battery> ().deviceBatteryIsAttachedTo = this.powerConsumer;

<<<<<<< HEAD
	// called from playerCharacter when Input.getkeydown KeyCode.E
	// sets the battery object active and attaches it to the power source 
	public void setBattery(GameObject battery){ // power consumer attach power source
		//this.battery = battery;
		// Sets in game battery object on to the switch
		Debug.Log ("BATTERY NAME: " + battery.name);
		battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
		battery.SetActive(true);
		battery.GetComponent<Battery> ().deviceBatteryIsAttachedTo = this.powerConsumer;


		// if there are no batteries with power
		if (battery.GetComponent<Battery> ().getPowerSource ().isEmpty ()) {
			Debug.Log ("Battery is empty!");
		} else {
			powerConsumer.attachPowerSource(battery.GetComponent<Battery>().getPowerSource());
		}

	}

	/// <summary>
	/// Perform initial activation work for this device upon recieving sufficient Power
	/// </summary>
	public void activate(){

		// new -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		if (this.GreenLights != null && !this.GreenLights.activeInHierarchy)
		{
			this.GreenLights.SetActive(true);
		}
		if (this.RedLights != null && this.RedLights.activeInHierarchy)
		{
			this.RedLights.SetActive(false);
		}
		// new -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
		// new -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		if (this.GreenLights != null && this.GreenLights.activeInHierarchy)
		{
			this.GreenLights.SetActive(false);
		}
		if (this.RedLights != null && !this.RedLights.activeInHierarchy)
		{
			this.RedLights.SetActive(true);
		}
		// new -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


		this.active = false;
		deviceController.deactivate ();
	}

	public bool isActive(){
		return active;
	}

	// NEW -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public Battery getBattery()
	{
		return this.battery;
	}
	// NEW -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

=======

		// if there are no batteries with power
		if (battery.GetComponent<Battery> ().getPowerSource ().isEmpty ()) {
			Debug.Log ("Battery is empty!");
		} else {
			powerConsumer.attachPowerSource(battery.GetComponent<Battery>().getPowerSource());
		}

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
		this.active = false;
		deviceController.deactivate ();
	}
		
	public bool isActive(){
		return active;
	}
>>>>>>> ede83ecdeb9a6a452e419e3821774de75aa84cff

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
