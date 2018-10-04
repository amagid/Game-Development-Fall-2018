using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comment for Git Testing, feel free to delete.

public class SwitchController : MonoBehaviour {

	private Inventory inventory;

	[SerializeField] private GameObject door;

	private bool atSwitch = false;

    public double xOffset;

    public double yOffset;

    public double zOffset;

	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (atSwitch) {
            if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 1)
            {
                if (useBattery()) {
                    door.GetComponent<DoorController>().StartCoroutine("openDoor");
                }
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

    bool useBattery()
    {
        GameObject battery = inventory.getFirstItem();
        battery.transform.position = transform.position + new Vector3((float)xOffset, (float)yOffset, (float)zOffset);
        battery.SetActive(true);
        if (battery.GetComponent<Battery>().isEmpty())
        {
            Debug.Log("Battery is empty!");
            return false;
        }
        battery.GetComponent<Battery>().StartCoroutine("useBattery");
        return true;
    }
}
