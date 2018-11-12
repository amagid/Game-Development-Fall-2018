using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour {
	[SerializeField] Sprite image;
	// the Content child object of ScrollView
	[SerializeField] GameObject content;
	// the DisplayData object of OnGameGUI
	[SerializeField] GameObject displayContent;

	private bool atNote = false;
    private NoteObjective objective = null;

	void Start(){
		displayContent.SetActive (false);
		Image img = content.GetComponent<Image> ();
		img.sprite = image;
        this.objective = this.GetComponent<NoteObjective>();
	}

	void Update(){
		if (atNote) {
			if (Input.GetKeyDown(KeyCode.E)){
				// display panel here w/ note
				SceneController.freezeGame();
				Image img = content.GetComponent<Image> ();
				img.sprite = image;
				displayContent.SetActive (true);
				atNote = false;
                if (this.objective != null)
                {
                    this.objective.complete();
                }
			}
		}
	}

	// Closes the content display panel
	public void closePanel(){
		displayContent.SetActive (false);
		SceneController.unfreezeGame();
	}

	void OnTriggerStay (Collider other) {
		if (other.name == "Player") {
			atNote = true;
		}
		
	}

	void OnTriggerExit(Collider other) {
		atNote = false;
	}
}
