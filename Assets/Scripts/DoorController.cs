using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    //private bool isOpen;
    private Vector3 closePos;
    private Vector3 openPos;

	private enum DoorAxes {X, Y, Z};
	[SerializeField] private DoorAxes doorAxisDirection;
	[SerializeField] private bool isElevatorDoor = false;

	private float CLOSE_FACTOR = 6f;

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

    public IEnumerator openDoor()
    {
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            transform.position = Vector3.Lerp(closePos, openPos, t);
            yield return null;
        }
        if (!isElevatorDoor)
        {
            yield return new WaitForSeconds(3f);
            for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
            {
                transform.position = Vector3.Lerp(openPos, closePos, t);
                yield return null;
            }
        }
        //isOpen = true;
    }


    public IEnumerator closeDoor()
    {
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            transform.position = Vector3.Lerp(openPos, closePos, t);
            yield return null;
        }
    }
}
