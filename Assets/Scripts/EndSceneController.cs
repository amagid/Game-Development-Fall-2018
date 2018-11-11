using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour {
	private bool atEndScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
		
		if (atEndScene && MachineController.activated) {
			Debug.Log ("*******************");
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
