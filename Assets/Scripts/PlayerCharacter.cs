using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
	private GUIStyle style1 = new GUIStyle();
    private Inventory inventory;

    void Start () {
		style1.fontSize = 25;
		style1.normal.textColor = Color.green;
        inventory = GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width - 160, 20, 200, 200), ("Batteries: " + inventory.itemCount()), style1);
	}

    //test for picking up batteries and use them for Test Scene One
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("battery"))
        {
            if (inventory.addItem(other.gameObject))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
