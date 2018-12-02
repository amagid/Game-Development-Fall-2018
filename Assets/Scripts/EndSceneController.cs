using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour {
	[SerializeField] private GameObject endLights;
	private bool atEndScene;
	private MachineController theReactor;

	void Start(){
		endLights.SetActive (false);
		theReactor = GameObject.Find ("Reactor").GetComponent<MachineController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;

		if (theReactor.getIsActivated()) {
			endLights.SetActive (true);
		}

		if (atEndScene && theReactor.getIsActivated()) {
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
