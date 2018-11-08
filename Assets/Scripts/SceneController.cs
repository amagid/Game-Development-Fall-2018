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
    private GameObject current_level;
    public bool lvl1_complete;
    public bool lvl2_complete;
    public bool game_complete;

    // Use this for initialization
    void Start () {
        current_level = level_one;
        lvl1_complete = false;
        lvl2_complete = false;
        game_complete = false;
	}

	// Update is called once per frame
	void Update () {
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
        //need a check for lvl1_complete
        lvl2_complete = lvl2_maze_controller.GetComponent<MazeController>().isComplete;
        //need a check for game_complete
    }

    public IEnumerator loadLevel(int level)
    {
        elevator_light.SetActive(false);
        yield return new WaitForSeconds(4f);
        current_level.SetActive(false);
        switch (level)
        {
            case 1:
                level_one.SetActive(true);
                break;
            case 2:
                level_two.SetActive(true);
                break;
            case 3:
                level_final.SetActive(true);
                break;
            default:
                throw new Exception("No Such Level");
                break;
        }
        StartCoroutine("elevatorMovingAnimation");
        yield return new WaitForSeconds(6f);
        elevator_door.GetComponent<DoorController>().StartCoroutine("openDoor");
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

    public void victory()
    {
        //do something if player completes the game
    }
}
