using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoorSwitch : MonoBehaviour {

    bool atSwitch = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (atSwitch)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("activate");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            atSwitch = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        atSwitch = false;
    }
}
