using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    //private bool isOpen;
    private Vector3 closePos;
    private Vector3 openPos;

    // Use this for initialization
    void Start () {
        this.closePos = transform.position;
        this.openPos = new Vector3(closePos.x - 6f, closePos.y, closePos.z);
	private enum DoorAxes {X, Y, Z};
	[SerializeField]
	private DoorAxes doorAxisDirection;

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
        yield return new WaitForSeconds(3f);
        for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
        {
            transform.position = Vector3.Lerp(openPos, closePos, t);
            yield return null;
        }
        //isOpen = true;
    }


    /* can add this in later if needed
    public IEnumerator closeDoor()
    {
        for(float t = 0f; t < 1; t += Time.deltaTime / 2f) {
            transform.position = Vector3.Lerp(openPos, closePos, t);
            yield return null;
        }
        //isOpen = false;
    }
    */
}
