﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, DirectOperation {

	//private bool isOpen;
	private Vector3 closePos;
	private Vector3 openPos;

	private enum DoorAxes {X, Y, Z};
	[SerializeField] private DoorAxes doorAxisDirection;
	[SerializeField] private bool isElevatorDoor = false;
	private bool active = false;

	private float CLOSE_FACTOR = 6f;
	private bool isSlowDoor = true;
	public void setIsSlowDoor(bool isSlowDoor){
		this.isSlowDoor = isSlowDoor;
	}

	// Use this for initialization
	void Start () {
		this.closePos = transform.position;
		Vector3 openPos = new Vector3 (closePos.x, closePos.y, closePos.z);
		switch (doorAxisDirection) {
		case DoorAxes.X:
			openPos.x -= CLOSE_FACTOR;
			break;
		case DoorAxes.Y:
			openPos.y -= CLOSE_FACTOR;
			break;
		case DoorAxes.Z:
			openPos.z -= CLOSE_FACTOR;
			break;
		}
		this.openPos = openPos;
		if (isElevatorDoor) {
			StartCoroutine("openDoor");
		}
	}

	// Update is called once per frame
	void Update () {

	}

<<<<<<< HEAD:Assets/Scripts/DoorController.cs
	private IEnumerator openDoor()
	{
		for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
		{
			transform.position = Vector3.Lerp(closePos, openPos, t);
			yield return null;
		}
		//isOpen = true;
	}
=======
    private IEnumerator openDoor()
    {
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            transform.position = Vector3.Lerp(closePos, openPos, t);
            yield return null;
        }
        //isOpen = true;
    }
>>>>>>> ede83ecdeb9a6a452e419e3821774de75aa84cff:Assets/Scripts/PoweredDevices/DoorController.cs


	private IEnumerator closeDoor()
	{
		for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
		{
			transform.position = Vector3.Lerp(openPos, closePos, t);
			yield return null;
		}
	}

	public void activate()
	{
		this.active = true;
		StartCoroutine(this.openDoor());
	}

	public void operate() {}

	public void deactivate()
	{
		this.active = false;
		StartCoroutine(this.closeDoor());
	}

	public bool isActive()
	{
		return this.active;
	}
}