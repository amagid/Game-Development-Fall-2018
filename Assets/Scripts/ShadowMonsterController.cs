using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMonsterController : MonoBehaviour {
    private PlayerCharacter player;
    private bool wandering = true;
    private float interval = 1.0f;
    private float distanceInterval = 1.0f;
    private Quaternion rotatingTo;
    private Vector3 movingTo;
    private float rotationSpeed = 0f;
    private float storedInterval;

	// Use this for initialization
	void Start () {
        this.interval += Random.value * 5;
        this.rotatingTo = this.transform.rotation;
        this.movingTo = this.transform.position;
        setMovementTarget();
	}

    private void Update()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.rotatingTo, this.rotationSpeed == 0f ? Time.deltaTime : this.rotationSpeed);
        this.transform.position = Vector3.Slerp(this.transform.position, this.movingTo, Time.deltaTime);
    }

    public void setPlayerCharacter(PlayerCharacter playerCharacter)
    {
        this.player = playerCharacter;
    }

    private void setMovementTarget()
    {
        if (this.player.getSanity() > 20)
        {
            this.rotationSpeed = 0f;
            this.rotatingTo = Quaternion.Euler(0, Random.value * 360 - 180, 0);
            this.movingTo = this.transform.position + this.transform.forward * Random.value * 10;
        } else
        {
            if (this.player.getSanity() == 20)
            {
                this.storedInterval = this.interval;
                this.distanceInterval = Vector3.Distance(this.player.transform.position, this.transform.position) / 20;
            }
            this.interval = this.storedInterval * (this.player.getSanity() / 20);
            this.rotationSpeed = Time.deltaTime / 10;
            this.rotatingTo = Quaternion.LookRotation(this.player.transform.position - this.transform.position, Vector3.up);
            float distance = this.distanceInterval * (this.player.getSanity() + 1);
            this.movingTo = this.player.transform.position - (this.transform.forward * distance);
        }
        Invoke("setMovementTarget", this.interval);
    }
}
