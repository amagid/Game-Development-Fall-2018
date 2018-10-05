using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour, PoweredOperation {
	private GameObject Player;
	private PlayerCharacter playersScript;
    private PowerConsumer powerConsumer;
	bool atLight;
	GameObject pointLight;
    private bool deviceActive;


	void Start () {
		Player = GameObject.Find ("Player");
		playersScript = (PlayerCharacter) Player.GetComponent(typeof(PlayerCharacter));
		pointLight = new GameObject("The Light");
		Light lightComp = pointLight.AddComponent<Light>();
		lightComp.intensity = 5;
		pointLight.transform.position = gameObject.transform.position;
		pointLight.SetActive (false);
        deviceActive = false;
        this.powerConsumer = new PowerConsumer(1, null);
	}

	// Update is called once per frame
	void Update () {

        // Activate/Operate/Deactivate device based on PowerConsumer state
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
                    this.powerConsumer.attachPowerSource(playersScript.getPowerSource());
                }
            }
            else
            {
                if (this.powerConsumer.hasPowerSource())
                {
                    this.powerConsumer.removePowerSource();
                }
            }
        }
        else
        {
            if (this.powerConsumer.hasPowerSource())
            {
                this.powerConsumer.removePowerSource();
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
            playersScript.gainSanity();
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

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atLight = true;
	}

	void OnTriggerExit(Collider other) {
        if (other.name == "Player")
    		atLight = false;
	}

}
