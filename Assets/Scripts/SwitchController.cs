using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comment for Git Testing, feel free to delete.

// Note: Powered operation interface 
public class SwitchController : MonoBehaviour {

	private Inventory inventory;

	[SerializeField] private GameObject door;

	private bool atSwitch = false;

    public double xOffset;

    public double yOffset;

    public double zOffset;

	// the battery that is attached to the switch
	private Battery battery = null;
	private PowerConsumer powerConsumer; 

	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
		this.powerConsumer = this.getPowerConsumer();
    }
	
	// Update is called once per frame
	void Update () {
		if (atSwitch) {
			if (battery != null) {
				Debug.Log (battery.name + " " + battery.getPowerSource().getPowerLevel());
			}
			if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 1 && battery == null)
			{
				if (useBattery()) { // NOTE: power consumer power device
					DoorController doorController = door.GetComponent<DoorController> ();
					this.battery.setDoorController (doorController);
					doorController.StartCoroutine("openDoor");
				}
			}
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

    bool useBattery()
    {
		GameObject batteryGO = inventory.getFirstItem();
		Debug.Log ("BATTERY NAME: " + batteryGO.name);
		batteryGO.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
		batteryGO.SetActive(true);
		if (batteryGO.GetComponent<Battery>().getPowerSource().isEmpty())
		{
			Debug.Log("Battery is empty!");
			return false;
		}
		this.battery = batteryGO.GetComponent<Battery>();
		this.battery.setSwitchController (this);
		door.GetComponent<DoorController> ().setIsSlowDoor (true);
		this.battery.StartCoroutine("useBattery");
		return true;
    }

	// Coppied from LightSourceController maybe should go in a parent class 
	public PowerConsumer getPowerConsumer()
	{
		PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
		if (pc == null)
		{
			throw new NoPowerConsumerException("LightSourceControllers must always have PowerConsumers! Please attach a PowerConsumer component in the Unity editor.");
		}
		return pc;
	}
}
