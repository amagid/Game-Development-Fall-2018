using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeController : MonoBehaviour {

    [SerializeField] private GameObject[] elevDoors;
    [SerializeField] private GameObject[] redDoors;
    [SerializeField] private GameObject[] greenDoors;
    [SerializeField] private GameObject[] blueDoors;
    private bool activated;

    // Use this for initialization
    void Start () {
        activated = false;
        setupInitialState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //setup the initial doors' state: All doors leading towards the elevator would be open
    void setupInitialState()
    {
        StartCoroutine(openDoor(elevDoors[0], "z"));
        StartCoroutine(openDoor(elevDoors[1], "z"));
        StartCoroutine(openDoor(elevDoors[2], "z"));
    }

    //method to activate maze
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !activated)
        {
            activateMaze();
            activated = true;
        }
    }

    void activateMaze()
    {
        StartCoroutine(closeDoor(elevDoors[0], "z"));
        StartCoroutine(closeDoor(elevDoors[1], "z"));
        StartCoroutine(closeDoor(elevDoors[2], "z"));
    }

    void deactivateMaze()
    {

    }

    public IEnumerator openDoor(GameObject door, string axis)
    {
        Vector3 closePos = door.transform.position;
        float openPosX = door.transform.position.x;
        float openPosY = door.transform.position.y;
        float openPosZ = door.transform.position.z;
        switch (axis) {
            case "x":
                openPosX += 7f;
                break;
            case "y":
                openPosY += 7f;
                break;
            case "z":
                openPosZ += 7f;
                break;
            default:
                throw new Exception("Invalid axis!");
                break;
        }
        Vector3 openPos = new Vector3(openPosX, openPosY, openPosZ);
        for (float t = 0f; t < 1; t += Time.deltaTime / 1.5f)
        {
            door.transform.position = Vector3.Lerp(closePos, openPos, t);
            yield return null;
        }
        door.SetActive(false);
    }

    public IEnumerator closeDoor(GameObject door, string axis)
    {
        Vector3 openPos = door.transform.position;
        float closePosX = door.transform.position.x;
        float closePosY = door.transform.position.y;
        float closePosZ = door.transform.position.z;
        switch (axis)
        {
            case "x":
                closePosX -= 7f;
                break;
            case "y":
                closePosY -= 7f;
                break;
            case "z":
                closePosZ -= 7f;
                break;
            default:
                throw new Exception("Invalid axis!");
                break;
        }
        Vector3 closePos = new Vector3(closePosX, closePosY, closePosZ);
        door.SetActive(true);
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            door.transform.position = Vector3.Lerp(openPos, closePos, t);
            yield return null;
        }
    }
}
