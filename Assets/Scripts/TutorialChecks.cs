using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecks : MonoBehaviour {

    [SerializeField] private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            switch (this.name)
            {
                case "CheckMovement":
                    Debug.Log("Checking Movement");
                    break;
                case "CheckPickup":
                    Debug.Log("Checking Pickup");
                    break;
                default:
                    break;
            }
        }
    }
}
