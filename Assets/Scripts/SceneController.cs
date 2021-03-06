﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject elevator_light;
    [SerializeField] private GameObject elevator_door;
    [SerializeField] private GameObject level_one;
    [SerializeField] private GameObject level_final;
    [SerializeField] private GameObject elevator_outside_lights;
    [SerializeField] private GameObject GameWinFader;

    private Vector3 elevator_outside_lights_initPos;
	// the player game object
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject camera;
	private static GameObject staticPlayer;
	private static GameObject staticCamera;
    public GameObject button_light;
    private GameObject current_level;
    public bool isElevatorMoving;
    public int current_level_num;
    private bool flickerOn;
    public bool lvl1_complete;
    public bool game_complete;
    public bool finished_tutorial;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        elevator_outside_lights_initPos = elevator_outside_lights.transform.position;
        flickerOn = true;
        current_level_num = 1;
        current_level = level_one;
        isElevatorMoving = false;
        lvl1_complete = false;
        game_complete = false;
		staticPlayer = player;
		staticCamera = camera;
        finished_tutorial = false;
	  }

	// Update is called once per frame
	void Update () {
        if (flickerOn)
        {
            elevatorLightFlicker();
        }
        else
        {
            elevator_light.SetActive(true);
        }
        //need a check for lvl1_complete

        //need a check for game_complete
    }

	// removes the player from the game temporarily so sanity/power doesn't decrease
	public void freezeGame()
    {
        Time.timeScale = 0f;
        player.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<PlayerCharacter>().enabled = false;
        player.GetComponent<PowerConsumer>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
		player.GetComponent<LowSanityEffects> ().enabled = false;
		player.GetComponent<FPSInput> ().enabled = false;
		player.GetComponent<Inventory> ().enabled = false;
		player.GetComponent<AudioSource> ().enabled = false;
        Cursor.visible = true;
	}

	public void unfreezeGame()
    {
        Time.timeScale = 1f;
        player.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<PowerConsumer>().enabled = true;
        player.GetComponent<PlayerCharacter>().enabled = true;
		player.GetComponent<LowSanityEffects> ().enabled = true;
		player.GetComponent<FPSInput> ().enabled = true;
		player.GetComponent<Inventory> ().enabled = true;
		player.GetComponent<AudioSource> ().enabled = true;
        Cursor.visible = false;
	}

    public IEnumerator finishGame()
    {
        isElevatorMoving = true;
        button_light.SetActive(true);
        flickerOn = false;
        elevator_door.GetComponent<DoorController>().StartCoroutine("closeDoor");
        yield return new WaitForSeconds(4f);
        current_level.SetActive(false);
        StartCoroutine("elevatorMovingAnimation", "UP");
        yield return new WaitForSeconds(2f);
        this.GameWinFader.SetActive(true);
        this.GameWinFader.GetComponent<CanvasRenderer>().SetAlpha(0f);
        this.GameWinFader.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetFloat("totalTime", Time.timeSinceLevelLoad);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");

    }

    public IEnumerator elevatorMovingAnimation(string direction) {
        elevator_outside_lights.transform.position = elevator_outside_lights_initPos;
        float initPosX = elevator_outside_lights.transform.position.x;
        float initPosY = elevator_outside_lights.transform.position.y;
        float initPosZ = elevator_outside_lights.transform.position.z;
        float finalPosX = initPosX;
        float finalPosY = initPosY;
        float finalPosZ = initPosZ;
        elevator_outside_lights.SetActive(true);
        switch (direction)
        {
            //elevator going UP, lights going down
            case "UP":
                finalPosY = initPosY - 20f;
                break;
            //elevator going DOWN, lights going up
            case "DOWN":
                finalPosY = initPosY;
                initPosY -= 20f;
                break;
            default:
                throw new Exception("No such directions for elevator lights");
        }
        Vector3 initPos = new Vector3(initPosX, initPosY, initPosZ);
        Vector3 finalPos = new Vector3(finalPosX, finalPosY, finalPosZ);
        for (int i = 0; i < 3; i++)
        {
            for (float t = 0f; t < 1; t += Time.deltaTime / 1f)
            {
                elevator_outside_lights.transform.position = Vector3.Lerp(initPos, finalPos, t);
                yield return null;
            }
            //moving the lights back to init position after it finished moving up
            elevator_outside_lights.transform.position = initPos;
            yield return new WaitForSeconds(0.7f);
        }
        elevator_outside_lights.SetActive(false);
    }

    private void elevatorLightFlicker()
    {
        //elevator light flicker effect
        if (UnityEngine.Random.value > 0.9)
        {
            if (elevator_light.active == true)
            {
                elevator_light.active = false;
            }
            else
            {
                elevator_light.active = true;
            }
        }
    }

    public void victory()
    {
        //do something if player completes the game
    }
}
