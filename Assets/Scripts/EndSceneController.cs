using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour {
	[SerializeField] private GameObject endLights;
	private bool atEndScene;

	void Start(){
		endLights.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;

		if (MachineController.activated) {
			endLights.SetActive (true);
		}

		if (atEndScene && MachineController.activated) {
			SceneManager.LoadScene ("EndGame");
		}
		
	}

	void OnTriggerStay(Collider other)
	{
		if (other.name == "Player")
		{
			atEndScene = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		atEndScene = false;
	}
}
