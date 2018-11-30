using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMonsterController : MonoBehaviour {
    [SerializeField] private GameObject bounds;
    private PlayerCharacter player;
    private bool wandering = true;
    private float interval = 3.0f;
    private float distanceInterval = 1.0f;
    private Quaternion rotatingTo;
    private Vector3 movingTo;
    private Quaternion rotatingFrom;
    private Vector3 movingFrom;
    private float rotationTime = 0f;
    private float movementTime = 1.0f;
    private float storedInterval;
    private bool stunned = false;
    private bool scattering = false;
    private bool inBounds = true;
    private float rotationTimeElapsed = 0.0f;
    private float movementTimeElapsed = 0.0f;
    private float distanceToPlayer;
    public float attackRange = 1f;
    public float attackAmount = 5f;
    public bool moving = true;

	// Use this for initialization
	void Start () {
        this.setPlayerCharacter(GameObject.Find("Player").GetComponent<PlayerCharacter>());
        this.interval += Random.value * 5;
        this.rotatingFrom = this.transform.rotation;
        this.movingFrom = this.transform.position;
        this.rotatingTo = this.transform.rotation;
        this.movingTo = this.transform.position;
        this.movementTime = 0.5f;
        this.rotationTime = 0.5f;
        this.resetRotationTimer();
        this.resetMovementTimer();
        //Get current distance to player
        this.distanceToPlayer = Vector3.Distance(this.player.transform.position, this.transform.position);
        this.setMovementTarget();
	}

    private void Update()
    {
        if (this.bounds == null || (inBounds && this.bounds.GetComponent<Collider>().bounds.Contains(this.movingTo)))
        {
            this.movementTimeElapsed += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.movingFrom, this.movingTo, this.getMovementProgress());
        }

        this.rotationTimeElapsed += Time.deltaTime;
        this.transform.rotation = Quaternion.Lerp(this.rotatingFrom, this.rotatingTo, this.getRotationProgress());

        if (this.bounds != null) {
            bool nowInBounds = this.bounds.GetComponent<Collider>().bounds.Contains(this.transform.position);
            if (!inBounds && nowInBounds)
            {
                this.inBounds = true;
            }
            else if (inBounds && !nowInBounds)
            {
                this.movingTo = this.transform.position + (this.bounds.GetComponent<Collider>().bounds.center - this.transform.position) / 5f;
                this.movingFrom = this.transform.position;
                this.resetMovementTimer();
                this.movementTime = this.interval;
                this.inBounds = false;
                Invoke("setMovementTarget", this.interval);
            }
        }
    }

    public void setPlayerCharacter(PlayerCharacter playerCharacter)
    {
        this.player = playerCharacter;
    }

    public void resetRotationTimer()
    {
        this.rotationTimeElapsed = 0.0f;
    }

    public void resetMovementTimer()
    {
        this.movementTimeElapsed = 0.0f;
    }

    public float getRotationProgress()
    {
        return this.rotationTimeElapsed / (this.rotationTime * ((this.player.getSanity() + 50) / 150));
    }

    public float getMovementProgress()
    {
        return this.movementTimeElapsed / (this.movementTime * ((this.player.getSanity() + 50) / 150));
    }

    private void setMovementTarget()
    {
        this.moving = true;
        if (!scattering && !stunned)
        {
            //Get current distance to player
            this.distanceToPlayer = Vector3.Distance(this.player.transform.position, this.transform.position);

            // If distance is less than our formula, stop and look at the player
            if (this.distanceToPlayer < Mathf.Pow(6 - (this.player.getSanity() / 20), 2))
            {
                this.resetRotationTimer();
                this.rotationTime = 0.25f;
                this.rotatingTo = Quaternion.LookRotation(this.player.transform.position - this.transform.position, Vector3.up);
                this.rotatingFrom = this.transform.rotation;
                this.moving = false;
            }

            // If distance is less than our second formula, start moving toward the player
            if ((this.bounds == null || this.inBounds) && this.distanceToPlayer < Mathf.Pow(6 - (this.player.getSanity() / 20), 2) / 1.5)
            {
                this.resetMovementTimer();
                this.movementTime = this.interval;
                this.movingTo = this.player.transform.position;
                this.movingFrom = this.transform.position;
                this.moving = true;
            }

            // If neither, wander around aimlessly
            if (this.distanceToPlayer >= Mathf.Pow(6 - (this.player.getSanity() / 20), 2))
            {
                if (this.player.getSanity() > 20)
                {
                    this.resetRotationTimer();
                    this.rotationTime = 0.5f;
                    this.rotatingTo = Quaternion.Euler(0, Random.value * 360 - 180, 0);
                    this.rotatingFrom = this.transform.rotation;
                    this.resetMovementTimer();
                    this.movementTime = this.interval;
                    this.movingTo = this.transform.position + this.rotatingTo * (this.transform.forward * (Random.value * 3 + 2));
                    this.movingFrom = this.transform.position;
                }
                else if (this.bounds == null)
                {
                    if (this.player.getSanity() == 20)
                    {
                        this.storedInterval = this.interval;
                        this.distanceInterval = Vector3.Distance(this.player.transform.position, this.transform.position) / 20;
                    }
                    this.interval = this.storedInterval * (this.player.getSanity() / 20);
                    this.resetRotationTimer();
                    this.rotationTime = 0.05f;
                    this.rotatingTo = Quaternion.LookRotation(this.player.transform.position - this.transform.position, Vector3.up);
                    this.rotatingFrom = this.transform.rotation;
                    float distance = this.distanceInterval * (this.player.getSanity() + 1);
                    this.resetMovementTimer();
                    this.movementTime = 0.05f;
                    this.movingTo = this.player.transform.position - this.rotatingTo * (this.transform.forward * distance);
                    this.movingFrom = this.transform.position;
                }
            }

            if (this.distanceToPlayer < this.attackRange)
            {
                this.player.getPowerSource().takePower(this.attackAmount);
                this.scatter(this.player.transform.position);
                return;
            }
            Invoke("setMovementTarget", this.interval);
        }
        else if (!scattering && stunned)
        {
            this.movingTo = this.transform.position;
            this.movingFrom = this.transform.position;
            this.resetRotationTimer();
            this.rotationTime = this.interval * 3;
            this.rotatingTo = Quaternion.LookRotation(this.player.transform.position - this.transform.position, Vector3.up);
            this.rotatingFrom = this.transform.rotation;
            Invoke("setMovementTarget", this.interval * 3);
        }
    }

    private void startStun()
    {
        this.stunned = true;
    }

    private void endStun()
    {
        this.stunned = false;
    }

    private void scatter(Vector3 fromPosition)
    {
        this.scattering = true;
        this.rotatingTo = Quaternion.LookRotation(this.transform.position - fromPosition, Vector3.up);
        this.rotatingFrom = this.transform.rotation;
        this.resetRotationTimer();
        this.rotationTime = 0.2f;
        float distance = 10f;
        this.resetMovementTimer();
        this.movementTime = this.interval * 2.5f;
        this.movingTo = this.transform.position + this.rotatingTo * (this.transform.forward * distance);
        this.movingFrom = this.transform.position;
        Invoke("stopScatter", this.interval * 5);
    }
    private void stopScatter() {
        this.scattering = false;
        this.setMovementTarget();
    }
}
