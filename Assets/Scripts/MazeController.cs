using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeController : MonoBehaviour {

    //Elevator doors move in the z axis
    [SerializeField] private GameObject[] elevDoors;
    //Red doors move in the x axis
    [SerializeField] private GameObject[] redDoors;
    //Green doors move in the z axis
    [SerializeField] private GameObject[] greenDoors;
    //Blue doors move in the x axis
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
        //close all elevator doors
        StartCoroutine(closeDoor(elevDoors[0], "z"));
        StartCoroutine(closeDoor(elevDoors[1], "z"));
        StartCoroutine(closeDoor(elevDoors[2], "z"));
        //start maze sequence, let the game begin
        StartCoroutine("startMazeSequence");
    }

    //RGB, BRG, GBR, RBG, BGR, GRB
    private IEnumerator startMazeSequence()
    {
        StartCoroutine("seqOne");
        yield return new WaitForSeconds(1f);
        StartCoroutine("seqTwo");
        yield return new WaitForSeconds(1f);
        StartCoroutine("seqThree");
        yield return new WaitForSeconds(1f);
        StartCoroutine("seqFour");
        yield return new WaitForSeconds(1f);
        StartCoroutine("seqFive");
        yield return new WaitForSeconds(1f);
        StartCoroutine("seqSix");
        yield return null;
    }

    //RGB
    private IEnumerator seqOne()
    {
        Debug.Log("RGB");
        StartCoroutine(openDoor(redDoors[0], "x"));
        StartCoroutine(openDoor(greenDoors[1], "z"));
        StartCoroutine(openDoor(blueDoors[2], "x"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(redDoors[0], "x"));
        StartCoroutine(closeDoor(greenDoors[1], "z"));
        StartCoroutine(closeDoor(blueDoors[2], "x"));
    }

    //BRG
    private IEnumerator seqTwo()
    {
        Debug.Log("BRG");
        StartCoroutine(openDoor(blueDoors[0], "x"));
        StartCoroutine(openDoor(redDoors[1], "x"));
        StartCoroutine(openDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(blueDoors[0], "x"));
        StartCoroutine(closeDoor(redDoors[1], "x"));
        StartCoroutine(closeDoor(greenDoors[2], "z"));
    }

    //GBR
    private IEnumerator seqThree()
    {
        Debug.Log("GBR");
        StartCoroutine(openDoor(greenDoors[0], "z"));
        StartCoroutine(openDoor(blueDoors[1], "x"));
        StartCoroutine(openDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(greenDoors[0], "z"));
        StartCoroutine(closeDoor(blueDoors[1], "x"));
        StartCoroutine(closeDoor(redDoors[2], "x"));
    }

    //RBG
    private IEnumerator seqFour()
    {
        Debug.Log("RBG");
        StartCoroutine(openDoor(redDoors[0], "x"));
        StartCoroutine(openDoor(blueDoors[1], "x"));
        StartCoroutine(openDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(redDoors[0], "x"));
        StartCoroutine(closeDoor(blueDoors[1], "x"));
        StartCoroutine(closeDoor(greenDoors[2], "z"));
    }

    //BGR
    private IEnumerator seqFive()
    {
        Debug.Log("BGR");
        StartCoroutine(openDoor(blueDoors[0], "x"));
        StartCoroutine(openDoor(greenDoors[1], "z"));
        StartCoroutine(openDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(blueDoors[0], "x"));
        StartCoroutine(closeDoor(greenDoors[1], "z"));
        StartCoroutine(closeDoor(redDoors[2], "x"));
    }

    //GRB
    private IEnumerator seqSix()
    {
        Debug.Log("GRB");
        StartCoroutine(openDoor(greenDoors[0], "z"));
        StartCoroutine(openDoor(redDoors[1], "x"));
        StartCoroutine(openDoor(blueDoors[2], "x"));
        yield return new WaitForSeconds(3f);
        StartCoroutine(closeDoor(greenDoors[0], "z"));
        StartCoroutine(closeDoor(redDoors[1], "x"));
        StartCoroutine(closeDoor(blueDoors[2], "x"));
    }

    //deactivate the maze by opening all the doors
    void deactivateMaze()
    {
        StartCoroutine(openDoor(elevDoors[0], "z"));
        StartCoroutine(openDoor(elevDoors[1], "z"));
        StartCoroutine(openDoor(elevDoors[2], "z"));
        StartCoroutine(openDoor(redDoors[0], "x"));
        StartCoroutine(openDoor(redDoors[1], "x"));
        StartCoroutine(openDoor(redDoors[2], "x"));
        StartCoroutine(openDoor(greenDoors[0], "z"));
        StartCoroutine(openDoor(greenDoors[1], "z"));
        StartCoroutine(openDoor(greenDoors[2], "z"));
        StartCoroutine(openDoor(blueDoors[0], "x"));
        StartCoroutine(openDoor(blueDoors[1], "x"));
        StartCoroutine(openDoor(blueDoors[2], "x"));
        activated = false;
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
