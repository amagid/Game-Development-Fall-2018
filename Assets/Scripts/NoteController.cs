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
    [SerializeField] private Inventory inventory;


	private bool atNote = false;
    private NoteObjective objective = null;

    private SceneController sceneController;

	void Start(){
        sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
		displayContent.SetActive (false);
		Image img = content.GetComponent<Image> ();
		img.sprite = image;
        this.objective = this.GetComponent<NoteObjective>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
	}

	void Update(){
		if (atNote) {
			if (Input.GetKeyDown(KeyCode.E)){
                openNote();
                inventory.addNote(this.gameObject);
			}
		}
	}

    public void openNote() {
        // display panel here w/ note
        sceneController.freezeGame();
        Image img = content.GetComponent<Image>();
        img.sprite = image;
		displayContent.transform.SetAsLastSibling ();
        displayContent.SetActive(true);
        atNote = false;
        if (this.objective != null)
        {
            this.objective.complete();
        }
        this.gameObject.SetActive(false);
    }

	// Closes the content display panel
	public void closePanel(){
		displayContent.SetActive (false);
		sceneController.unfreezeGame();
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
