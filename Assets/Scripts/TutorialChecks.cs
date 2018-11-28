using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecks : MonoBehaviour {

    private GameObject player;

    private PlayerCharacter player_character;

    private bool hasReducedSanity = false;

    private GameObject[] allDialogues;

    private GameObject dialogue_canvas;

    [SerializeField]private GameObject currentDialogue;

    private Inventory inventory;

    private SceneController scene_controller;


    // Use this for initialization
    void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.player_character = this.player.GetComponent<PlayerCharacter>();
        this.dialogue_canvas = GameObject.Find("DialogueCanvas");
        this.allDialogues = GameObject.FindGameObjectsWithTag("dialogue");
        this.inventory = player.GetComponent<Inventory>();
        this.scene_controller = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void hideAllDialogues()
    {
        foreach(GameObject dialogue in allDialogues)
        {
            dialogue.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            switch (this.name)
            {
                case "CheckMovement":
                    player_character.gainSanity(0.05f);
                    break;
                case "TutorialInstructions":
                    player_character.gainSanity(0.05f);
                    break;
                case "CheckPickup":
                    player_character.gainSanity(0.05f);
                    break;
                case "CheckNoteReading":
                    player_character.gainSanity(0.05f);
                    break;
                case "CheckDoorOpening":
                    player_character.gainSanity(0.05f);
                    break;
                default:
                    player_character.resumeSanityLoss();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !scene_controller.finished_tutorial)
        {
            switch (this.name)
            {
                case "CheckMovement":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "TutorialInstructions":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckPickup":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckNoteReading":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckDoorOpening":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckPersonalLight":
                    if (!hasReducedSanity)
                    {
                        player_character.reduceSanity(15f);
                        hasReducedSanity = true;
                    }
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckTurnOnLamp":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckChargeFromFreezer":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckChargeFromBattery":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CheckCrouch":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "TutorialFinal":
                    hideAllDialogues();
                    currentDialogue.SetActive(true);
                    break;
                case "CloseAll":
                    hideAllDialogues();
                    inventory.clearItems();
                    scene_controller.finished_tutorial = true;
                    break;
                default:
                    break;
            }
        }
    }
}
