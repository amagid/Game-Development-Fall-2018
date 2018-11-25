using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecks : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        this.player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            switch (this.name)
            {
                case "CheckMovement":
                    Debug.Log("Welcome Player! Use WASD to move around!");
                    break;
                case "TutorialInstructions":
                    Debug.Log("");
                    break;
                case "CheckPickup":
                    Debug.Log("Checking Pickup");
                    break;
                case "CheckNoteReading":
                    Debug.Log("Checking NoteReading");
                    break;
                case "CheckDoorOpening":
                    Debug.Log("Checking DoorOpening");
                    break;
                case "CheckPersonalLight":
                    Debug.Log("");
                    break;
                case "CheckTurnOnLamp":
                    Debug.Log("");
                    break;
                case "CheckChargeFromFreezer":
                    Debug.Log("");
                    break;
                case "CheckChargeFromBattery":
                    Debug.Log("");
                    break;
                case "CheckCrouch":
                    Debug.Log("");
                    break;
                default:
                    break;
            }
        }
    }
}
