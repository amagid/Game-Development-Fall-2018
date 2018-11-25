using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScenePlayer : MonoBehaviour {
    [SerializeField] private GameObject headBag;
    [SerializeField] private GameObject kickSound;
    [SerializeField] private GameObject shaftBottom;
    [SerializeField] private GameObject blackout;
    [SerializeField] private float dragTime = 1.0f;
    [SerializeField] private float bagRemoveTime = 1.0f;
    [SerializeField] private float lookTime = 1.0f;
    [SerializeField] private float kickTime = 1.0f;
    [SerializeField] private float tipTime = 1.0f;
    [SerializeField] private float fallTime = 1.0f;
    [SerializeField] private float fallAcceleration = 4.9f;
    [SerializeField] private float crashTime = 0.1f;
    [SerializeField] private float crashBlackoutTime = 3.0f;
    private float movementTimeLimit;
    private float rotationTimeLimit;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool moving = false;
    private bool rotating = false;
    private bool bagMoving = false;
    private bool falling = false;
    private float movementTimeElapsed = 0.0f;
    private float rotationTimeElapsed = 0.0f;
    private float fallingTimeElapsed = 0.0f;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        this.startDrag();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.moving)
        {
            this.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, this.getMovementTime(this.movementTimeLimit));
        }

        if (this.rotating)
        {
            this.transform.rotation = Quaternion.Lerp(this.startRotation, this.endRotation, this.getRotationTime(this.rotationTimeLimit));
        }

        if (this.bagMoving)
        {
            this.headBag.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, this.getMovementTime(this.movementTimeLimit));
        }

        // Needs different timing formula since you accelerate
        if (this.falling)
        {
            this.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, this.getFallingTime(this.movementTimeLimit));
        }
    }

    void startDrag()
    {
        this.movementTimeLimit = this.dragTime;
        this.startPosition = this.transform.position;
        this.endPosition = this.transform.position + new Vector3(7.5f, 0, 0);
        this.moving = true;
        this.resetMovementTime();
        Invoke("startRemoveBag", this.dragTime);
    }

    void startRemoveBag()
    {
        this.moving = false;
        this.resetMovementTime();
        this.movementTimeLimit = this.bagRemoveTime;
        this.startPosition = this.headBag.transform.position;
        this.endPosition = this.headBag.transform.position + new Vector3(0, 1.0f, 0);
        this.bagMoving = true;
        this.headBag.GetComponent<AudioSource>().Play();
        Invoke("startKick", this.bagRemoveTime);
    }

    /*

    void startLookLeft()
    {
        this.bagMoving = false;
        this.headBag.SetActive(false);
        this.resetMovementTime();
        this.resetRotationTime();
        this.rotationTimeLimit = this.lookTime;
        this.startRotation = this.transform.rotation;
        this.endRotation = Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(0, -70, 0));
        this.rotating = true;
        Invoke("startLookRight", this.lookTime);
    }

    void startLookRight()
    {
        this.rotating = false;
        this.resetRotationTime();
        this.rotationTimeLimit = this.lookTime;
        this.startRotation = this.transform.rotation;
        this.endRotation = Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(0, 140, 0));
        this.rotating = true;
        Invoke("startLookCenter", this.lookTime);
    }

    void startLookCenter()
    {
        this.rotating = false;
        this.resetRotationTime();
        this.rotationTimeLimit = this.lookTime;
        this.startRotation = this.transform.rotation;
        this.endRotation = Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(0, -140, 0));
        this.rotating = true;
        Invoke("startKick", this.lookTime * 0.5f);
    }

    */

    void startKick()
    {
        this.bagMoving = false;
        this.headBag.SetActive(false);
        this.rotating = false;
        this.kickSound.GetComponent<AudioSource>().Play();
        Invoke("startKickMovement", 0.4f);
    }

    void startKickMovement() {
        this.resetRotationTime();
        this.rotationTimeLimit = this.kickTime;
        this.startRotation = this.transform.rotation;
        this.endRotation = Quaternion.Euler(new Vector3(-50, 90, 0));
        this.rotating = true;
        Invoke("startTipping", this.kickTime);
    }

    void startTipping()
    {
        this.rotating = false;
        this.moving = false;
        this.resetRotationTime();
        this.resetMovementTime();
        this.rotationTimeLimit = this.tipTime;
        this.movementTimeLimit = this.tipTime;
        this.startRotation = this.transform.rotation;
        this.startPosition = this.transform.position;
        this.endRotation = Quaternion.Euler(new Vector3(90, 90, 0));
        this.endPosition = this.transform.position + new Vector3(1.5f, -4.0f, 0);
        this.rotating = true;
        this.moving = true;
        Invoke("startFalling", this.tipTime);
    }

    void startFalling()
    {
        this.rotating = false;
        this.moving = false;
        this.falling = false;
        this.resetFallingTime();
        this.movementTimeLimit = this.fallTime;
        this.startPosition = this.transform.position;
        this.endPosition = this.transform.position + new Vector3(0, this.shaftBottom.transform.position.y - this.transform.position.y + 2f, 0);
        this.falling = true;
        Invoke("crash", Mathf.Sqrt(this.fallTime / this.fallAcceleration) - this.crashTime);
    }

    void crash()
    {
        this.shaftBottom.GetComponent<AudioSource>().Play(0);
        Invoke("startBlackout", this.crashTime);
    }

    void startBlackout()
    {
        this.falling = false;
        this.blackout.SetActive(true);
        Invoke("finishUp", this.crashBlackoutTime);
    }

    void finishUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene1");
    }

    void resetMovementTime()
    {
        this.movementTimeElapsed = 0.0f;
    }

    void resetRotationTime()
    {
        this.rotationTimeElapsed = 0.0f;
    }

    void resetFallingTime()
    {
        this.fallingTimeElapsed = 0.0f;
    }

    float getMovementTime(float limit)
    {
        this.movementTimeElapsed += Time.deltaTime;
        return this.movementTimeElapsed / limit;
    }

    float getRotationTime(float limit)
    {
        this.rotationTimeElapsed += Time.deltaTime;
        return this.rotationTimeElapsed / limit;
    }

    float getFallingTime(float limit)
    {
        this.fallingTimeElapsed += Time.deltaTime;
        return this.fallAcceleration * Mathf.Pow(this.fallingTimeElapsed, 2) / limit;
    }
}
