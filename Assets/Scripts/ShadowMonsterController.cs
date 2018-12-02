﻿using System.Collections;
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
    private bool stunned = false;
    public bool scattering = false;
    public bool inBounds = true;
    private float rotationTimeElapsed = 0.0f;
    private float movementTimeElapsed = 0.0f;
    private float distanceToPlayer;
    public float attackRange = 1f;
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

        Debug.Log(this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z);
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
        return this.rotationTimeElapsed / (this.rotationTime * ((this.player.getSanity() + 50) / 150));
    }

    public float getMovementProgress()
    {
        return this.movementTimeElapsed / (this.movementTime * ((this.player.getSanity() + 50) / 150));
    }

    public void setMovementTarget()
    {
        if (this.dyingOrSpawning || this.dead)
        {
            return;
        }
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

    public void scatter(Vector3 fromPosition)
    {
        Debug.Log("Scattering");
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
        Invoke("setMovementTarget", this.movementTime);
        Invoke("finishSpawn", this.movementTime);
    }

    public void finishSpawn()
    {
        this.dyingOrSpawning = false;
        this.GetComponent<Collider>().enabled = true;
    }

    public void die()
    {
        this.die(true);
    }

    public void die(bool respawn)
    {
        Debug.Log("Dying");
        this.scattering = false;
        this.GetComponent<Collider>().enabled = false;
        this.dyingOrSpawning = true;
        this.resetMovementTimer();
        this.movementTime = 3f;
        this.movingFrom = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.movingTo = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
        if (respawn)
        {
            Invoke("respawn", this.movementTime);
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
    }

    public void respawn()
    {
        this.spawn();
    }
}
