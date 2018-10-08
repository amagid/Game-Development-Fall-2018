using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField] private float personalLightSanityRate;
    [SerializeField] private float personalLightPowerRate;
    [SerializeField] private float starting_power;
    [SerializeField] private GameObject central_lighting;
    private GUIStyle style1 = new GUIStyle();
    private Inventory inventory;
	private const float MAX_SANITY = 100f;
    private const float SANITY_DECREASE_RATE = 0.05f;
    private const float ELEVATOR_SANITY_RATE = 0.1f;

	private float sanity;
	private bool losingSanity;
    private bool inElevator;
    private PowerSource internalBattery;
    private PowerConsumer internalPowerConsumer;
    private Light personalLight;

    void Start () {
		style1.fontSize = 25;
		style1.normal.textColor = Color.green;
        inventory = GetComponent<Inventory>();
		sanity = MAX_SANITY;
		losingSanity = true;
        inElevator = false;

        internalBattery = new PowerSource(300f, starting_power);
        this.internalPowerConsumer = this.gameObject.GetComponent<PowerConsumer>();
        if (this.internalPowerConsumer == null)
        {
            throw new NoPowerConsumerException("Player must have a PowerConsumer! Please add a PowerConsumer component to the Player in the Unity editor.");
        }
        this.internalPowerConsumer.attachPowerSource(this.internalBattery);

        this.personalLight = this.GetComponentInChildren<Camera>().gameObject.GetComponentInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (sanity <= 0 || !this.internalPowerConsumer.powerDevice())
        {
            endGame();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sanity += personalLightSanityRate;
            if (sanity > MAX_SANITY)
            {
                sanity = MAX_SANITY;
            }
            this.internalBattery.takePower(personalLightPowerRate);
            this.personalLight.enabled = true;
            losingSanity = false;
        } else
        {
            this.personalLight.enabled = false;
        }

        //added a check if the central lighting in ON, player would not lose sanity
        if(central_lighting.activeSelf)
        {
            losingSanity = false;
        }

		if (losingSanity && !inElevator) {
			sanity -= SANITY_DECREASE_RATE;
		} else if (inElevator)
        {
            this.gainSanity(ELEVATOR_SANITY_RATE);
        }
        losingSanity = true;
	}

    
	void OnGUI() {
		GUI.Label (new Rect (Screen.width - 160, 0, 200, 200), ("Batteries: " + inventory.itemCount()), style1);
		GUI.Label (new Rect (Screen.width - 160, 20, 200, 200), ("Power: " + Mathf.RoundToInt(internalBattery.getPowerLevel())), style1);
		GUI.Label (new Rect (Screen.width - 160, 40, 200, 200), ("Sanity: " + Mathf.RoundToInt(sanity)), style1);	
	}
    
    

    //test for picking up batteries and use them for Test Scene One
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("battery"))
        {
			Battery battery = other.gameObject.GetComponent<Battery> ();
			if (battery.isInUse ()) {
				Debug.Log ("Player took battery too early");
				//				Debug.Log(battery.getDoorController ().gameObject.name);
				DoorController doorController = battery.getDoorController ();
				if (doorController != null) {
					doorController.setIsSlowDoor (false);
					battery.getSwitchController ().setBattery (null);
					battery.setDoorController (null);
				} else {
					Debug.Log ("TURNING OFF THE LIGHTS!!!");
					CentralLightController centralLightController = battery.getCentralLightController ();
					centralLightController.StartCoroutine ("turnOffLights");
					centralLightController.setBatteryOnSwitch (null);
					battery.setCentralLightController (null);
				}
				battery.setIsInUse (false);
			}
            if (inventory.addItem(other.gameObject))
            {
                other.gameObject.SetActive(false);
            }
        } else if (other.gameObject.CompareTag("note"))
        {
            Debug.Log(other.gameObject.GetComponent<NoteController>().GetContent());
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("zone_collider"))
        {
            if (other.gameObject.name == "ElevatorCollider")
            {
                Debug.Log("Player at Elevator");
                inElevator = true;
            }
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

}
