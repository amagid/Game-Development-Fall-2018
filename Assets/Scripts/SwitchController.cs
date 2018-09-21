using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comment for Git Testing, feel free to delete.

public class SwitchController : MonoBehaviour {

	private Inventory inventory;

	private bool atSwitch = false;

	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (atSwitch) {
			if (Input.GetKeyDown (KeyCode.E) && inventory.itemCount() == 3)
            {
                useAllBatteries();
            }
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.name == "Player")
			atSwitch = true;
	}

	void OnTriggerExit(Collider other) {
		atSwitch = false;
	}

    void useAllBatteries()
    {
        GameObject battery1 = inventory.getFirstItem();
        battery1.transform.position = new Vector3(9.7f, 2.7f, 23.5f);
        battery1.SetActive(true);
        GameObject battery2 = inventory.getFirstItem();
        battery2.transform.position = new Vector3(9.0f, 2.7f, 23.5f);
        battery2.SetActive(true);
        GameObject battery3 = inventory.getFirstItem();
        battery3.transform.position = new Vector3(8.3f, 2.7f, 23.5f);
        battery3.SetActive(true);
    }
}
