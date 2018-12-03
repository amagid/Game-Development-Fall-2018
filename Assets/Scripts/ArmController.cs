using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject visionMarker;
    public float movementTime = 0.5f;
    private Vector3 initialPosition;
    private bool extending = false;
    private bool retracting = false;
    private float movementTimeElapsed = 0f;
    private float movementTimeLimit = 0f;

	// Use this for initialization
	void Start () {
        this.initialPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion lookDirection = Quaternion.LookRotation(this.visionMarker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Euler(this.camera.transform.rotation.eulerAngles.x, lookDirection.eulerAngles.y, this.camera.transform.rotation.eulerAngles.z);
        if (extending)
        {
            Vector3 destination = this.transform.parent.InverseTransformVector(this.visionMarker.transform.position - this.transform.position);
            this.transform.localPosition = Vector3.Lerp(this.initialPosition, new Vector3((this.initialPosition.x + destination.x) / 2, (this.initialPosition.y + destination.y) / 4 + 0.5f, (this.initialPosition.z + destination.z) / 2), this.getMovementTime());
        } else if (retracting)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.initialPosition, this.getMovementTime());
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            this.extend();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.retract();
        }
    }

    public void resetMovementTimer()
    {
        this.movementTimeElapsed = 0f;
    }

    public float getMovementTime()
    {
        this.movementTimeElapsed += Time.deltaTime;
        return this.movementTimeElapsed / this.movementTimeLimit;
    }

    public bool extend()
    {
        Debug.Log("Extending");
        if (retracting || extending)
        {
            return false;
        }
        this.resetMovementTimer();
        this.movementTimeLimit = this.movementTime;
        this.extending = true;
        Invoke("finishExtend", this.movementTime);
        return true;
    }

    private void finishExtend()
    {
        Debug.Log("Finished Extending");
        this.extending = false;
        this.retract();
    }

    public bool retract()
    {
        Debug.Log("Retracting");
        if (retracting || extending)
        {
            return false;
        }
        this.resetMovementTimer();
        this.movementTimeLimit = this.movementTime;
        this.retracting = true;
        Invoke("finishRetract", this.movementTime);
        return true;
    }

    private void finishRetract()
    {
        Debug.Log("Finished Retracting");
        this.retracting = false;
    }
}
