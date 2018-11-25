﻿using System.Collections;
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
                    Debug.Log("This is the tutorial. " +
                        "Your sanity level is indicated on the left bar; power level on the right." +
                        "The bottom bar is a quick inventory menu." +
                        "Make sure you complete each objective in this tutorial before you proceed!");
                    break;
                case "CheckPickup":
                    Debug.Log("Batteries can be picked up using E when you are close. Pick up this battery!");
                    break;
                case "CheckNoteReading":
                    Debug.Log("The purple tile in front of you is a Note. Press E to read a note when you are close.");
                    break;
                case "CheckDoorOpening":
                    Debug.Log("Doors are controlled by switches. Get close to the switch on your right. " +
                        "Press E to place a battery on the switch to activate the door.");
                    break;
                case "CheckPersonalLight":
                    Debug.Log("While in the dark, your sanity drops quite a bit. " +
                        "Press F to toggle your personal light to stop your sanity from decreasing." +
                        "Using the personal Light will consume your power");
                    break;
                case "CheckTurnOnLamp":
                    Debug.Log("Light sources will bring your sanity back up." +
                        "Point the crosshair towards the lamp and hold LMB to power it until your sanity becomes full again.");
                    break;
                case "CheckChargeFromFreezer":
                    Debug.Log("Your power is low. " +
                        "You can siphon a small amount of power from a freezer by getting close and holding RMB.");
                    break;
                case "CheckChargeFromBattery":
                    Debug.Log("Pick up the battery in front of you." +
                        "Press I to open up the inventory UI." +
                        "Click on the battery and press the Charge button until it fills up your power.");
                    break;
                case "CheckCrouch":
                    Debug.Log("Most obstacles can be overcome by crouching and walking through it." +
                        "Press C to crouch and again to stand up when you are clear." +
                        "Beyond this obstacle is the inside of the facility. Are you ready to complete your mission?");
                    break;
                default:
                    break;
            }
        }
    }
}
