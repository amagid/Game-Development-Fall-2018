using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, DirectOperation {

	//private bool isOpen;
	private Vector3 closePos;
	private Vector3 openPos;
    private Vector3 curPos;

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
        this.curPos = transform.position;
        this.closePos = curPos;
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
        curPos = transform.position;
	}

	private IEnumerator openDoor()
	{
		for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
		{
            transform.position = Vector3.Lerp(curPos, openPos, t);
			yield return null;
		}
	}


	private IEnumerator closeDoor()
	{
		for (float t = 0f; t < 1; t += Time.deltaTime / 1f)
		{
            transform.position = Vector3.Lerp(curPos, closePos, t);
            yield return null;
		}
	}

    private IEnumerator closeDoorForPower()
    {
        for (float t = 0f; t < 1; t += Time.deltaTime / 0.7f)
        {
            transform.position = Vector3.Lerp(curPos, closePos, t);
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
        StopAllCoroutines();
        StartCoroutine(this.closeDoorForPower());
	}

	public bool isActive()
	{
		return this.active;
	}
}
