using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour {
	private GameObject Player;
	private PlayerCharacter playersScript;
    private PowerConsumer powerConsumer;
	bool atLight;
	GameObject pointLight;


	void Start () {
		Player = GameObject.Find ("Player");
		playersScript = (PlayerCharacter) Player.GetComponent(typeof(PlayerCharacter));
		pointLight = new GameObject("The Light");
		Light lightComp = pointLight.AddComponent<Light>();
		lightComp.intensity = 5;
		pointLight.transform.position = gameObject.transform.position;
		pointLight.SetActive (false);
        this.powerConsumer = new PowerConsumer(1, null);
	}

	// Update is called once per frame
	void Update () {
        if (this.powerConsumer.powerDevice())
        {
            this.operate();
        } else
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

    private void operate()
    {
        pointLight.SetActive(true);
        if (atLight)
        {
            playersScript.gainSanity();
        }
    }

    private void deactivate()
    {
        if (pointLight.activeInHierarchy)
        {
            pointLight.SetActive(false);
        }
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
