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
    //the indicator light of the switch
    [SerializeField] private Light indicator;
    //the current sequence number
    private int seqnum;
    //if the maze has been activated
    private bool activated;
    //if the player picks up all 3 parts
    public bool isComplete;

    // Use this for initialization
    void Start () {
        indicator.color = Color.white;
        seqnum = 1;
        activated = false;
        isComplete = false;
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

    //method to check if the player enters the center room
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if(!activated)
            {
                closeDoorsBehind();
            }
        }
    }

    private void closeDoorsBehind()
    {
        //close all elevator doors
        StartCoroutine(closeDoor(elevDoors[0], "z"));
        StartCoroutine(closeDoor(elevDoors[1], "z"));
        StartCoroutine(closeDoor(elevDoors[2], "z"));
        indicator.color = Color.red;
    }

    //changing the door sequence when called
    //Sequence order: RGB, BRG, GBR, RBG, BGR, GRB
    public void nextMazeSequence()
    {
        //if the player picks up all 3 parts, deactivate the maze
        if(isComplete)
        {
            deactivateMaze();
            return;
        }

        switch (this.seqnum)
        {
            case 1:
                StartCoroutine("seqOne");
                seqnum++;
                break;
            case 2:
                StartCoroutine("seqTwo");
                seqnum++;
                break;
            case 3:
                StartCoroutine("seqThree");
                seqnum++;
                break;
            case 4:
                StartCoroutine("seqFour");
                seqnum++;
                break;
            case 5:
                StartCoroutine("seqFive");
                seqnum++;
                break;
            case 6:
                StartCoroutine("seqSix");
                seqnum = 1;
                break;
            default:
                throw new Exception("invalid sequence number");
                break;
        }
    }

    //RGB
    private IEnumerator seqOne()
    {
        Debug.Log("RGB");
        indicator.color = Color.green;
        if (activated)
        {
            StartCoroutine(closeDoor(greenDoors[0], "z"));
            StartCoroutine(closeDoor(redDoors[1], "x"));
            StartCoroutine(closeDoor(blueDoors[2], "x"));
            activated = true;
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(openDoor(redDoors[0], "x"));
        StartCoroutine(openDoor(greenDoors[1], "z"));
        StartCoroutine(openDoor(blueDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
    }

    //BRG
    private IEnumerator seqTwo()
    {
        Debug.Log("BRG");
        indicator.color = Color.green;
        StartCoroutine(closeDoor(redDoors[0], "x"));
        StartCoroutine(closeDoor(greenDoors[1], "z"));
        StartCoroutine(closeDoor(blueDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(openDoor(blueDoors[0], "x"));
        StartCoroutine(openDoor(redDoors[1], "x"));
        StartCoroutine(openDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
    }

    //GBR
    private IEnumerator seqThree()
    {
        Debug.Log("GBR");
        indicator.color = Color.green;
        StartCoroutine(closeDoor(blueDoors[0], "x"));
        StartCoroutine(closeDoor(redDoors[1], "x"));
        StartCoroutine(closeDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(openDoor(greenDoors[0], "z"));
        StartCoroutine(openDoor(blueDoors[1], "x"));
        StartCoroutine(openDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
    }

    //RBG
    private IEnumerator seqFour()
    {
        Debug.Log("RBG");
        indicator.color = Color.green;
        StartCoroutine(closeDoor(greenDoors[0], "z"));
        StartCoroutine(closeDoor(blueDoors[1], "x"));
        StartCoroutine(closeDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(openDoor(redDoors[0], "x"));
        StartCoroutine(openDoor(blueDoors[1], "x"));
        StartCoroutine(openDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
    }

    //BGR
    private IEnumerator seqFive()
    {
        Debug.Log("BGR");
        indicator.color = Color.green;
        StartCoroutine(closeDoor(redDoors[0], "x"));
        StartCoroutine(closeDoor(blueDoors[1], "x"));
        StartCoroutine(closeDoor(greenDoors[2], "z"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(openDoor(blueDoors[0], "x"));
        StartCoroutine(openDoor(greenDoors[1], "z"));
        StartCoroutine(openDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
    }

    //GRB
    private IEnumerator seqSix()
    {
        Debug.Log("GRB");
        indicator.color = Color.green;
        StartCoroutine(closeDoor(blueDoors[0], "x"));
        StartCoroutine(closeDoor(greenDoors[1], "z"));
        StartCoroutine(closeDoor(redDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(openDoor(greenDoors[0], "z"));
        StartCoroutine(openDoor(redDoors[1], "x"));
        StartCoroutine(openDoor(blueDoors[2], "x"));
        yield return new WaitForSeconds(2f);
        indicator.color = Color.red;
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
        indicator.color = Color.white;
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
        for (float t = 0f; t < 1; t += Time.deltaTime / 1.5f)
        {
            door.transform.position = Vector3.Lerp(openPos, closePos, t);
            yield return null;
        }
    }
}
