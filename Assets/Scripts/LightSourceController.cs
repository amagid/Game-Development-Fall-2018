using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour {
	private GameObject Player;
	private PlayerCharacter playersScript;
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
	}

	// Update is called once per frame
	void Update () {
		if (atLight) {
			if (Input.GetKey (KeyCode.E)){
				pointLight.SetActive (true);
				playersScript.reducePower ();
				playersScript.gainSanity ();
			}
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atLight = true;
	}

	void OnTriggerExit(Collider other) {
		atLight = false;
	}

}
