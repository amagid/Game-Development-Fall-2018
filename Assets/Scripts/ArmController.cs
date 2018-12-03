using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject visionMarker;
    private Animator animator;
    public float movementTime = 0.5f;
    private Vector3 initialPosition;
    private bool extending = false;
    private bool retracting = false;
    private float movementTimeElapsed = 0f;
    private float movementTimeLimit = 0f;
    private bool poking = false;

	// Use this for initialization
	void Start () {
        this.initialPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
        this.animator = this.GetComponent<Animator>();
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
        this.extending = false;
        this.retract();
    }

    public bool retract()
    {
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
        this.retracting = false;
    }

    public void startPoke()
    {
        if (poking)
        {
            return;
        }
        this.poking = true;
        this.animator.SetBool("pushbutton", true);
        this.extend();
        Invoke("finishPoke", this.movementTime);
    }

    public void finishPoke()
    {
        this.poking = false;
        this.retract();
        this.animator.SetBool("pushbutton", false);
    }
}
