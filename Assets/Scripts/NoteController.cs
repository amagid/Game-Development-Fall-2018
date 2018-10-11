using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {
    [SerializeField] string content;
	[SerializeField] GameObject displayContent;
	[SerializeField] GameObject player;
	[SerializeField] GameObject secondaryCameraGO;

	private bool atNote = false;

    public string GetContent()
    {
        return this.content;
    }

	void Start(){
		displayContent.SetActive (false);
		secondaryCameraGO.SetActive (false);
	}

	void Update(){
		if (atNote) {
			if (Input.GetKeyDown(KeyCode.E)){
				// display panel here w/ note
				freezeGame();
				displayContent.SetActive (true);
				atNote = false;
			}
		}
	}

	// removes the player from the game temporarily so sanity/power doesn't decrease
	void freezeGame(){
		GameObject camera = player.transform.Find("Camera").gameObject;
		secondaryCameraGO.transform.position = camera.transform.position;
		secondaryCameraGO.transform.rotation = camera.transform.rotation;
		secondaryCameraGO.transform.localEulerAngles = camera.transform.localEulerAngles;
		player.SetActive (false);
		secondaryCameraGO.SetActive (true);
	}

	void unfreezeGame(){
		secondaryCameraGO.SetActive (false);
		player.SetActive (true);
	}

	// Closes the content display panel
	public void closePanel(){
		displayContent.SetActive (false);
		unfreezeGame();
	}

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atNote = true;
	}

	void OnTriggerExit(Collider other) {
		atNote = false;
	}
}
