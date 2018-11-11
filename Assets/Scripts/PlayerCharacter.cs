using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCharacter : MonoBehaviour {
	[SerializeField] private float personalLightPowerRate;
	[SerializeField] private float starting_power;
	[SerializeField] private GameObject central_lighting;
	[SerializeField] private float powerSiphonRate;
	[SerializeField] private Camera camera;
	[SerializeField] private float interactionRange = 2.5f;
	private GUIStyle style1 = new GUIStyle();
	private GUIStyle style2 = new GUIStyle();
	private GUIStyle style3 = new GUIStyle();
	private Inventory inventory;
	private const float MAX_SANITY = 100f;
	[SerializeField] private float personalLightSanityRate = 0.02f;
	private const float SANITY_DECREASE_RATE = 0.05f;
	private const float ELEVATOR_SANITY_RATE = 0.1f;

	private float sanity;
	private bool losingSanity;
	private bool inElevator;
	private bool isCrouching;
	private bool isPersonalLightOn = false;
	private bool canStandUp;
	private PowerSource internalBattery;
	private PowerConsumer internalPowerConsumer;
	private Light personalLight;
	private GameObject seenObject;
	private PowerConsumer currentConsumer;
	private PowerSource currentSource;
	private string cursorMessage;
    private bool personalLightOn = false;

	void Start () {
		style1.fontSize = 25;
		style1.normal.textColor = Color.green;

		style2.fontSize = 25;
		style2.normal.textColor = Color.white;

		style3.fontSize = 16;
		style3.normal.textColor = Color.white;
		style3.alignment = TextAnchor.UpperCenter;

		inventory = GetComponent<Inventory>();
		sanity = MAX_SANITY;
		losingSanity = true;
		inElevator = false;
		isCrouching = false;
		canStandUp = true;
		internalBattery = new PowerSource(300f, starting_power);
		this.internalPowerConsumer = this.gameObject.GetComponent<PowerConsumer>();
		if (this.internalPowerConsumer == null)
		{
			throw new NoPowerConsumerException("Player must have a PowerConsumer! Please add a PowerConsumer component to the Player in the Unity editor.");
		}
		this.internalPowerConsumer.attachPowerSource(this.internalBattery);

		this.personalLight = this.GetComponentInChildren<Camera>().gameObject.GetComponentInChildren<Light>();
        this.personalLight.enabled = false;
		InvokeRepeating("sanityChange", 1f, 0.1f);
	}

	//invoke repeat method for general dark area decrease, elevator increase and personal light increase
	void sanityChange() {
		if(this.personalLight.enabled) {
			sanity += personalLightSanityRate;
			if (sanity > MAX_SANITY)
			{
				sanity = MAX_SANITY;
			}
		}
		else if (losingSanity && !inElevator)
		{
			sanity -= SANITY_DECREASE_RATE;
		}
		else if (inElevator)
		{
			this.gainSanity(ELEVATOR_SANITY_RATE);
		}
	}

	// Update is called once per frame
	void Update () {
		losingSanity = true;
		if (sanity <= 0 || !this.internalPowerConsumer.powerDevice())
		{
			endGame();
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
			if (isPersonalLightOn) {
				this.personalLight.enabled = false;
				isPersonalLightOn = false;
			} else {
				this.personalLight.enabled = true;
				isPersonalLightOn = true;
			}
        }
		if (isPersonalLightOn) {
			this.internalBattery.takePower(personalLightPowerRate);
			losingSanity = false;
		}

		//the crouch function
		if (Input.GetKeyDown(KeyCode.C))
		{
			//if crouching
			if (isCrouching && canStandUp)
			{
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
				//transform.position = new Vector3(transform.position.x, transform.position.y * 2f, transform.position.z);
				isCrouching = false;
			}
			//if standing
			else if (!isCrouching)
			{
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2f, transform.localScale.z);
				transform.position = new Vector3(transform.position.x, transform.position.y / 2f, transform.position.z);
				isCrouching = true;
			}
		}

        //added a check if the central lighting in ON, player would not lose sanity
        if (central_lighting.activeSelf)
		{
			losingSanity = false;
		}
			
		// Raycast to find powerable objects
		RaycastHit hit;

		// Cast Ray in current direction of Camera
		Transform cameraPos = this.GetComponentInChildren<Camera>().transform;

		// If we've let go of the LMB or gone too far from the device, disconnect from the device we're powering
		if (this.currentConsumer != null && (!Input.GetKey(KeyCode.Mouse0) || Vector3.Distance(this.currentConsumer.transform.position, this.transform.position) > this.interactionRange * 1.5f))
		{
			this.currentConsumer.removePowerSource();
			this.currentConsumer = null;
		}

		// If our RayCast hits an object
		if (Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, this.interactionRange))
		{
			if (this.seenObject != hit.collider.gameObject)
			{
				this.updateSeenObject(hit.collider.gameObject);
			}

			// If the left mouse button is being pressed, attempt to power the hit device
			if (Input.GetKey(KeyCode.Mouse0) && this.currentConsumer == null)
			{
				// Get the device's PowerConsumer, if any
				PowerConsumer pc = hit.collider.gameObject.GetComponent<PowerConsumer>();

				// If the PowerConsumer exists, check if it's the same one we were on last tick.
				if (pc != null)
				{
					if (pc.attachPowerSource(this.internalBattery))
					{
						this.currentConsumer = pc;
					}
					// If the PowerConsumer did not exist, then we looked away so we need to disconnect from our previous PowerConsumer
				}
				// If we get too far away, stop powering device
				// TODO: Figure out why 2 * interactionRange is needed - we get a blinking effect without it.
			}

			//If the right mouse button is being pressed, attempt to siphon power from the hit device.
			if (Input.GetKey(KeyCode.Mouse1))
			{
				// If we have a power source from last tick, siphon some of its power.
				if (this.currentSource != null)
				{
					bool result = PowerSource.transferPower(this.currentSource, this.internalBattery, this.powerSiphonRate);
				}
				// Get the PowerConsumer of the object we're looking at, if any.
				PowerConsumer pc = hit.collider.gameObject.GetComponent<PowerConsumer>();
				// If there is a PowerConsumer on this object, check if its PowerSource is the same one from last tick.
				if (pc != null)
				{
					PowerSource ps = pc.getPowerSource();
					// If the PowerSource is different from last tick, reassign our current power source.
					// This includes the case of a null PowerSource - in that case, we will stop siphoning power, as expected.
					if (ps != this.currentSource)
					{
						this.currentSource = ps;
					}
					// If the PowerConsumer was null, then there is no source to siphon power from, so make sure we don't keep siphoning from the last PowerSource.
				}
				else
				{
					this.currentSource = null;
				}
				// If we're no longer pressing RMB, stop siphoning power.
			}
			else
			{
				this.currentSource = null;
			}

			//If the E key is being pressed, try to add a battery to the seen device.
			if (Input.GetKeyDown(KeyCode.E))
			{
				// Get the PowerConsumer of the object we're looking at, if any.
				PowerConsumer pc = hit.collider.gameObject.GetComponent<PowerConsumer>();
				// If there is a PowerConsumer on this object, check if it has a PowerSource.
				if (pc != null)
				{
					PowerSource ps = pc.getPowerSource();
					// If the PowerConsumer did not have a PowerSource, attach the first battery in our inventory.
					if (ps == null)
					{
						GameObject gameObjectPC = this.inventory.getSelectedItem();
						Battery batteryPC = gameObjectPC != null ? gameObjectPC.GetComponent<Battery>() : null;
						if (batteryPC != null && batteryPC.getPowerSource() != null)
						{
							if(pc.isOneTimeActivation()){
								inventory.addItem (gameObjectPC);
							}
							pc.attachPowerSource(batteryPC.getPowerSource());
					
							SwitchController sc = hit.collider.gameObject.GetComponent<SwitchController>();
							if (sc != null)
							{
								sc.setBattery(gameObjectPC);
							}
						}
						// if the power consumer is one time use return the battery to the character

						// If the PowerConsumer did have a PowerSource, check if it's extractable & take it if it is
					} else if (pc.isPowerSourceExtractable() && !pc.isOneTimeActivation())
					{
						PowerSource oldPC = pc.removePowerSource();
						Battery battery = oldPC.theBattery;

						inventory.addItem(battery.gameObject); 
						battery.gameObject.SetActive(false);

					}
				}
			}
			// If our RayCast did not hit any objects, then we're not looking at anything, so stop siphoning / giving Power to things.
		}
		else
		{
			this.updateSeenObject(null);
			this.currentSource = null;
		}
	}
		
	void OnGUI()
	{

		GUI.Label (new Rect (Screen.width - 160, 0, 200, 200), ("Batteries: " + inventory.itemCount()), style1);
		GUI.Label (new Rect (Screen.width - 160, 20, 200, 200), ("Power: " + Mathf.RoundToInt(internalBattery.getPowerLevel())), style1);
		GUI.Label (new Rect (Screen.width - 160, 40, 200, 200), ("Sanity: " + Mathf.RoundToInt(sanity)), style1);	

		GUI.Label(new Rect(Screen.width / 2 - 13, Screen.height / 2 - 13, 26, 26), "+", style2);
		GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 10, 200, 60), this.cursorMessage, style3);
	}
		
	//test for picking up batteries and use them for Test Scene One
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("battery"))
		{
			if (other.gameObject.CompareTag("battery"))
			{
				Battery battery = other.gameObject.GetComponent<Battery> ();
				if (battery.deviceBatteryIsAttachedTo != null) {
					battery.deviceBatteryIsAttachedTo.removePowerSource ();
					battery.deviceBatteryIsAttachedTo = null;
					Debug.Log ("Player took battery too early");
				}
				inventory.addItem(other.gameObject); 
				other.gameObject.SetActive(false);

			} 
		} 
		else if (other.gameObject.CompareTag("note"))
		{
			//TODO: add note to player's inventory??
			//Debug.Log(other.gameObject.GetComponent<NoteController>().GetContent());
			//other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("zone_collider"))
		{
			if (other.gameObject.name == "ElevatorCollider")
			{
				inElevator = true;
			}
		}

		// If we enter a crouch only zone, set canStandUp to false
		if (other.gameObject.CompareTag("crouch_only"))
		{
			this.canStandUp = false;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("zone_collider"))
		{
			if (other.gameObject.name == "ElevatorCollider")
			{
				inElevator = false;
			}
		}

		// If we enter a crouch only zone, set canStandUp to false
		if (other.gameObject.CompareTag("crouch_only"))
		{
			this.canStandUp = true;
		}
	}

	public float getPower(){
		return internalBattery.getPowerLevel();
	}

	public void reducePower() {
		if (!internalBattery.takePower(1)) {
			endGame();
		}
	}

	public void gainSanity(float amount){
		this.sanity += amount;
		if (this.sanity > MAX_SANITY) {
			this.sanity = MAX_SANITY;
		}
		this.losingSanity = false;
	}

	public void endGame(){
		SceneManager.LoadScene("GameOver");
	}

	public Inventory GetInventory() {
		return this.inventory;
	}

	public float getSanity() {
		return sanity;
	}

	public PowerSource getPowerSource() {
		return internalBattery;
	}

	public void pauseSanityLoss()
	{
		this.losingSanity = false;
	}

	private void updateSeenObject(GameObject obj)
	{
		this.seenObject = obj;
		if (obj != null)
		{
			this.cursorMessage = obj.name;
			PowerConsumer pc = obj.GetComponent<PowerConsumer>();
			if (pc != null && !obj.CompareTag("Switch"))
			{
				if (pc.isOneTimeActivation())
				{
					this.cursorMessage += "\nRequires " + pc.getActivationThreshold() + " Power to Activate";
				} else if (pc.getConsumptionRate() > 0)
				{
					this.cursorMessage += "\nRequires " + (pc.getConsumptionRate() * 60f).ToString("F2") + " Power/Second";
				}
				if (pc.getConsumptionRate() > 0 || (pc.isOneTimeActivation() && pc.getActivationThreshold() > 0))
				{
					this.cursorMessage += "\nLMB - Give Power ";
				}
				if (pc.getPowerSource() != null && pc.getPowerSource() != this.internalBattery)
				{
					this.cursorMessage += "\nRMB - Siphon Power";
				}
			}
			if (obj.CompareTag("battery") || obj.CompareTag("note"))
			{
				this.cursorMessage += "\nE - Pick Up";
			}
			if (obj.CompareTag("Switch"))
			{
				if (pc != null && ((obj.GetComponent<SwitchController>() != null && obj.GetComponent<SwitchController>().getBattery() == null) || (obj.GetComponent<CentralLightController>() != null))) // change here might be wrong the last boolean statment should be battery == null 
				{
					this.cursorMessage += "\nNeeds Battery";
					if (this.inventory.itemCount() > 0) // TODO: Do this better (right now will break if we add items that aren't batteries to the game)
					{
						this.cursorMessage += "\nE - Attach Battery";
					}
				}
			}
		} else
		{
			this.cursorMessage = "";
		}
	}

}
