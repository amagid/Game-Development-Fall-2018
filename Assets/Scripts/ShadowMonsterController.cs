using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMonsterController : MonoBehaviour {
    public GameObject bounds;
    private PlayerCharacter player;
    private bool wandering = true;
    public float interval = 1.0f;
    private float distanceInterval = 1.0f;
    public Quaternion rotatingTo;
    public Vector3 movingTo;
    public Quaternion rotatingFrom;
    public Vector3 movingFrom;
    public float rotationTime = 0f;
    public float movementTime = 1.0f;
    private float storedInterval;
    public bool endingStun = false;
    public bool scattering = false;
    public bool inBounds = true;
    private float rotationTimeElapsed = 0.0f;
    private float movementTimeElapsed = 0.0f;
    private float distanceToPlayer;
    public float attackRange = 4f;
    public float attackAmount = 5f;
    public bool moving = true;
    public bool movingBackToBounds = false;
    [System.NonSerialized] public Vector3 initialPosition;
    public bool dead = false;
    public bool dyingOrSpawning = false;
    private int boundsDeathTimer = 0;
    public bool inFlashlight = false;
    public float lifespan = 0.0f;

	// Use this for initialization
	void Start () {
        this.setPlayerCharacter(GameObject.Find("Player").GetComponent<PlayerCharacter>());
        this.interval += Random.value * 2;
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
        this.initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        this.spawn();
        if (this.lifespan > 0f)
        {
            this.lifespan = this.lifespan * 0.75f + this.lifespan * Random.value;
            Invoke("die", this.lifespan);
        }
	}

    private void Update()
    {
        if (this.player.getSanity() <= 0f && this.bounds != null)
        {
            this.bounds = null;
            this.inBounds = true;
            this.resetMovementTimer();
            this.movementTime = 6f;
            this.setMovementTarget();
        }
        if (this.dead || (this.inFlashlight && !this.scattering && this.player.personalLight.enabled))
        {
            return;
        }

        if (!this.inBounds)
        {
            this.boundsDeathTimer++;
            if (this.boundsDeathTimer > 300)
            {
                this.respawn();
            }
        } else
        {
            this.boundsDeathTimer = 0;
        }

        if (this.bounds == null || (inBounds && (this.bounds.GetComponent<Collider>().bounds.Contains(this.movingTo) || this.scattering)) || this.movingBackToBounds || this.dyingOrSpawning)
        {
            this.movementTimeElapsed += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.movingFrom, this.movingTo, this.getMovementProgress());
        } else if (this.bounds != null && !this.inBounds && !this.movingBackToBounds)
        {
            this.movingTo = this.transform.position + (this.bounds.GetComponent<Collider>().bounds.center - this.transform.position) / 3f;
            this.movingFrom = this.transform.position;
            this.resetMovementTimer();
            this.movementTime = this.interval;
            this.movingBackToBounds = true;
        }

        this.rotationTimeElapsed += Time.deltaTime;
        this.transform.rotation = Quaternion.Lerp(this.rotatingFrom, this.rotatingTo, this.getRotationProgress());
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
        return this.rotationTimeElapsed / (this.rotationTime * ((this.player.getSanity() + 200) / 300));
    }

    public float getMovementProgress()
    {
        return this.movementTimeElapsed / (this.movementTime * ((this.player.getSanity() + 200) / 300));
    }

    public void setMovementTarget()
    {
        if (this.dyingOrSpawning || this.dead)
        {
            return;
        }
        this.moving = true;
        if (!scattering && !inFlashlight)
        {
            //Get current distance to player
            this.distanceToPlayer = Vector3.Distance(this.player.transform.position, this.transform.position);

            // If distance is less than our formula, stop and look at the player
            if (this.distanceToPlayer < Mathf.Pow(7 - (this.player.getSanity() / 20), 2) || this.player.getSanity() <= 0f)
            {
                this.resetRotationTimer();
                this.rotationTime = 0.25f;
                this.rotatingTo = Quaternion.LookRotation(this.transform.position - this.player.transform.position, Vector3.up);
                this.rotatingFrom = this.transform.rotation;
                this.moving = false;
            }

            // If distance is less than our second formula, start moving toward the player
            if ((this.bounds == null || this.inBounds) && this.distanceToPlayer < Mathf.Pow(7 - (this.player.getSanity() / 20), 2) / 1.5 || this.player.getSanity() <= 0f)
            {
                this.resetMovementTimer();
                this.movementTime = this.interval;
                this.movingTo = this.player.transform.position;
                this.movingFrom = this.transform.position;
                this.moving = true;
            }

            // If neither, wander around aimlessly
            if (this.distanceToPlayer >= Mathf.Pow(7 - (this.player.getSanity() / 20), 2))
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

            if (this.distanceToPlayer < this.attackRange && this.player.getSanity() > 0)
            {
                this.player.getPowerSource().takePower(this.attackAmount);
                this.player.reduceSanity(this.attackAmount);
                this.scatter(this.player.transform.position);
                return;
            }
            Invoke("setMovementTarget", this.interval);
        }
        else if (!scattering && inFlashlight)
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

    public void startStun()
    {
        this.inFlashlight = true;
        this.endingStun = false;
    }

    public void endStun()
    {
        if (this.endingStun)
            this.inFlashlight = false;
    }

    public void scatter(Vector3 fromPosition)
    {
        this.scattering = true;
        this.rotatingTo = Quaternion.LookRotation(this.transform.position - fromPosition, Vector3.up);
        this.rotatingFrom = this.transform.rotation;
        this.resetRotationTimer();
        this.rotationTime = 0.2f;
        float distance = 100f;
        this.resetMovementTimer();
        this.movementTime = this.interval;
        this.movingTo = this.transform.position + (this.rotatingTo * this.transform.forward) * distance;
        this.movingFrom = this.transform.position;
        Invoke("die", this.interval);
    }

    public void spawn()
    {
        this.GetComponent<Collider>().enabled = false;
        this.dead = false;
        this.dyingOrSpawning = true;
        this.transform.position = new Vector3(this.initialPosition.x, this.player.transform.position.y - 10f, this.initialPosition.z);
        this.rotatingFrom = this.transform.rotation;
        this.rotatingTo = Quaternion.Euler(new Vector3(0, this.transform.rotation.eulerAngles.y, 0));
        this.resetRotationTimer();
        this.rotationTime = 0.1f;
        this.movingFrom = new Vector3(this.initialPosition.x, this.player.transform.position.y - 10f, this.initialPosition.z);
        this.resetMovementTimer();
        this.movementTime = 5f;
        this.movingTo = new Vector3(this.initialPosition.x, this.player.transform.position.y + Random.value - 0.5f, this.initialPosition.z);
        this.inBounds = true;
        Invoke("finishSpawn", this.movementTime);
    }

    public void finishSpawn()
    {
        this.dyingOrSpawning = false;
        this.GetComponent<Collider>().enabled = true;
        this.setMovementTarget();
    }

    public void die()
    {
        this.die(true);
    }

    public void die(bool respawn)
    {
        this.scattering = false;
        this.GetComponent<Collider>().enabled = false;
        this.dyingOrSpawning = true;
        this.resetMovementTimer();
        this.movementTime = 3f;
        this.movingFrom = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.movingTo = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
        if (respawn)
        {
            Invoke("respawn", this.movementTime + 5f);
        }
        else
        {
            Invoke("finishDeath", this.movementTime);
        }
    }

    public void finishDeath()
    {
        this.dead = true;
        this.dyingOrSpawning = false;
        this.GetComponent<Collider>().enabled = true;
        this.gameObject.SetActive(false);
        Object.Destroy(this.gameObject);
    }

    public void respawn()
    {
        GameObject newMonster = GameObject.Instantiate(this.player.shadowMonsterPrefab, this.initialPosition, Quaternion.Euler(0, 0, 0), this.transform.parent) as GameObject;
        newMonster.GetComponent<ShadowMonsterController>().bounds = this.bounds;
        this.finishDeath();
    }

    public void startReactorKill(GameObject reactor)
    {
        this.movingFrom = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.movingTo = reactor.transform.position;
        this.dyingOrSpawning = true;
        this.movementTime = 5f + Random.value;
        this.resetMovementTimer();
        Invoke("finishDeath", this.movementTime);
    }
}
