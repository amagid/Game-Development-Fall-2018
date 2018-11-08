using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject elevator_light;
    [SerializeField] private GameObject elevator_door;
    [SerializeField] private GameObject level_one;
    [SerializeField] private GameObject level_two;
    [SerializeField] private GameObject level_final;
    [SerializeField] private GameObject elevator_outside_lights;
    [SerializeField] private GameObject lvl2_maze_controller;
	// the player game object
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject camera;
	private static GameObject staticPlayer;
	private static GameObject staticCamera;
    public GameObject button_light;
    private GameObject current_level;
    public bool isElevatorMoving;
    public int current_level_num;
    private bool elevatorLightOn;
    public bool lvl1_complete;
    public bool lvl2_complete;
    public bool game_complete;

    // Use this for initialization
    void Start () {
        elevatorLightOn = true;
        current_level_num = 1;
        current_level = level_one;
        isElevatorMoving = false;
        lvl1_complete = false;
        lvl2_complete = false;
        game_complete = false;
		staticPlayer = player;
		staticCamera = camera;
	}

	// Update is called once per frame
	void Update () {
        if (elevatorLightOn)
        {
            elevatorLightFlicker();
        }
        else
        {
            elevator_light.SetActive(false);
        }
        //need a check for lvl1_complete
        lvl2_complete = lvl2_maze_controller.GetComponent<MazeController>().isComplete;
        //need a check for game_complete
    }

	// removes the player from the game temporarily so sanity/power doesn't decrease
	public static void freezeGame(){
		Time.timeScale = 0;
		staticPlayer.GetComponent<FPSInput> ().enabled = false;
		staticPlayer.GetComponent<MouseLook> ().enabled = false;
		staticPlayer.GetComponent<PlayerCharacter> ().enabled = false;
		staticCamera.GetComponent<MouseLook> ().enabled = false;

	}

	public static void unfreezeGame(){
		Time.timeScale = 1;
		staticPlayer.GetComponent<FPSInput> ().enabled = true;
		staticPlayer.GetComponent<MouseLook> ().enabled = true;
		staticPlayer.GetComponent<PlayerCharacter> ().enabled = true;
		staticCamera.GetComponent<MouseLook> ().enabled = false;
	}

    public IEnumerator loadLevel(int level)
    {
        isElevatorMoving = true;
        button_light.SetActive(true);
        elevatorLightOn = false;
        elevator_door.GetComponent<DoorController>().StartCoroutine("closeDoor");
        yield return new WaitForSeconds(4f);
        current_level.SetActive(false);
        switch (level)
        {
            case 1:
                level_one.SetActive(true);
                current_level_num = 1;
                current_level = level_one;
                break;
            case 2:
                level_two.SetActive(true);
                current_level_num = 2;
                current_level = level_two;
                break;
            case 3:
                level_final.SetActive(true);
                current_level_num = 3;
                current_level = level_final;
                break;
            default:
                throw new Exception("No Such Level");
                break;
        }
        StartCoroutine("elevatorMovingAnimation");
        yield return new WaitForSeconds(6f);
        elevator_door.GetComponent<DoorController>().StartCoroutine("openDoor");
        isElevatorMoving = false;
        elevatorLightOn = true;
        button_light.SetActive(false);
    }

    public IEnumerator elevatorMovingAnimation() {
        for (int i = 0; i < 3; i++)
        {
            elevator_outside_lights.SetActive(true);
            Vector3 initPos = elevator_outside_lights.transform.position;
            float x = elevator_outside_lights.transform.position.x;
            float y = elevator_outside_lights.transform.position.y - 20f;
            float z = elevator_outside_lights.transform.position.z;
            Vector3 finalPos = new Vector3(x, y, z);
            for (float t = 0f; t < 1; t += Time.deltaTime / 1f)
            {
                elevator_outside_lights.transform.position = Vector3.Lerp(initPos, finalPos, t);
                yield return null;
            }
            //moving the lights back to init position after it finished moving up
            elevator_outside_lights.transform.position = initPos;
            elevator_outside_lights.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
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
