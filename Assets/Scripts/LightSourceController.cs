using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour, PoweredOperation {
    [SerializeField] float sanityIncreaseRate;
	private GameObject Player;
	private PlayerCharacter playersScript;
	bool atLight;
	GameObject pointLight;
    private bool deviceActive;
    private PowerConsumer powerConsumer;

	void Start () {
		Player = GameObject.Find ("Player");
		playersScript = (PlayerCharacter) Player.GetComponent(typeof(PlayerCharacter));
		pointLight = new GameObject("The Light");
		Light lightComp = pointLight.AddComponent<Light>();
		lightComp.intensity = 5;
		pointLight.transform.position = gameObject.transform.position + new Vector3(0.8f, 0.8f);
		pointLight.SetActive (false);
        deviceActive = false;
        this.powerConsumer = this.getPowerConsumer();
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
        // Activate/Operate/Deactivate device based on powerConsumer state
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

        // TODO: Move all powering code like this to central location such as PlayerCharacter script
        if (atLight)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!this.powerConsumer.hasPowerSource())
                {
                    //this.powerConsumer.attachPowerSource(playersScript.getPowerSource());
                }
            }
            else
            {
                if (this.powerConsumer.hasPowerSource())
                {
                    //this.powerConsumer.removePowerSource();
                }
            }
        }
        else
        {
            if (this.powerConsumer.hasPowerSource())
            {
                //this.powerConsumer.removePowerSource();
            }
        }
	}

    public void activate()
    {
        pointLight.SetActive(true);
        this.deviceActive = true;
    }

    public void operate()
    {
        if (atLight)
        {
            playersScript.gainSanity(this.sanityIncreaseRate);
        }
    }

    public void deactivate()
    {
        if (pointLight.activeInHierarchy)
        {
            pointLight.SetActive(false);
        }
        deviceActive = false;
    }

    public bool isActive()
    {
        return deviceActive;
    }

    public PowerConsumer getPowerConsumer()
    {
        PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
        if (pc == null)
        {
            throw new NoPowerConsumerException("LightSourceControllers must always have PowerConsumers! Please attach a PowerConsumer component in the Unity editor.");
        }
        return pc;
    }

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atLight = true;
	}

	void OnTriggerExit(Collider other) {
        if (other.name == "Player")
    		atLight = false;
	}

}
