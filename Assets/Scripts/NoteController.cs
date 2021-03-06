﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour {
	public Sprite image;
	// the Content child object of ScrollView
	[SerializeField] GameObject content;
	// the DisplayData object of OnGameGUI
	[SerializeField] GameObject displayContent;
    [SerializeField] private Inventory inventory;
    // for tutorial note only
    [SerializeField] private GameObject dialogue1;
    [SerializeField] public GameObject dialogue2;
    private TabletUI tablet;

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
        tablet = GameObject.Find("Tablet").GetComponent<TabletUI>();
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
        if (dialogue1 != null)
        {
            dialogue1.SetActive(false);
        }
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
        if (dialogue2 != null)
        {
            dialogue2.SetActive(true);
        }
        if (tablet.openedNoteFromInventory)
        {
            tablet.resumeDataPanel();
        }
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
