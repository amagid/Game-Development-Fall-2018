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
	public bool isElevatorDoor = false;
	private bool active = false;
	private float CLOSE_FACTOR = 6f;
	private bool isSlowDoor = true;
    private bool isClosing = false;
    private bool isOpening = false;
    private AudioSource source;
    public AudioClip doorOpen;

	public void setIsSlowDoor(bool isSlowDoor){
		this.isSlowDoor = isSlowDoor;
	}

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
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
        //curPos = transform.position;
	}

	private IEnumerator openDoor()
	{
    	source.PlayOneShot(doorOpen, 0.15f);
        isOpening = true;
        if (!isElevatorDoor)
        {
            yield return new WaitForSeconds(1f);
        }
        for (float t = 0f; t < 1; t += Time.deltaTime / 20f)
        {
			transform.position = Vector3.Lerp(gameObject.transform.position, openPos, t);
            yield return null;
        }
        isOpening = false;
	}


	private IEnumerator closeDoor()
	{
		Debug.Log ("SUP BITCH BOI");
			source.PlayOneShot (doorOpen, 0.15f);
			isClosing = true;
			if (!isElevatorDoor) {
				yield return new WaitForSeconds (0f);
			}
			for (float t = 0f; t < 1; t += Time.deltaTime / 6f) {
			transform.position = Vector3.Lerp (gameObject.transform.position, closePos, t);
				yield return null;
			}
			isClosing = false;
	}

	public void activate()
	{
		this.active = true;
		StopAllCoroutines ();
		StartCoroutine(this.openDoor());
        
	}

	public void operate() {}

	public void deactivate()
	{
		this.active = false;
		StopAllCoroutines ();
        StartCoroutine(this.closeDoor());
        
    }

	public bool isActive()
	{
		return this.active;
	}
}
