using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSanityEffects : MonoBehaviour {

    [SerializeField] private GameObject player;

    [SerializeField] private Camera camera;

    private float sanity;

	// Use this for initialization
	void Start () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
	}
	
	// Update is called once per frame
	void Update () {
        sanity = player.GetComponent<PlayerCharacter>().getSanity();
    }

    //camera shaking
    public void cameraShake() {

    }

    //decrease range of sight
    public void decreaseSight() {

    }

    //change player's self emitting light's color

    //player will be able to solve a level easier if his sanity level is high

    //cubes appearing in random places
    public void randomCubes() {

    }
}
