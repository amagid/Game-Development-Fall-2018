using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCharacter : MonoBehaviour {
	private GUIStyle style1 = new GUIStyle();
    private Inventory inventory;
	private const int MAX_SANITY = 1000;
	private int sanity;
	private bool losingSanity;
    private bool inElevator;
    private PowerSource internalBattery;

    void Start () {
        internalBattery = new PowerSource(300f, 100f);
		style1.fontSize = 25;
		style1.normal.textColor = Color.green;
        inventory = GetComponent<Inventory>();
		sanity = MAX_SANITY;
		losingSanity = true;
        inElevator = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (internalBattery.isEmpty())
        {
            endGame();
        }
		if (losingSanity && !inElevator) {
			//sanity--;
			if (sanity < 0) {
				endGame ();
			}
		}
	}

    /*
	void OnGUI() {
		GUI.Label (new Rect (Screen.width - 160, 0, 200, 200), ("Batteries: " + inventory.itemCount()), style1);
		GUI.Label (new Rect (Screen.width - 160, 20, 200, 200), ("Power: " + Mathf.RoundToInt(internalBattery.getPowerLevel())), style1);
		GUI.Label (new Rect (Screen.width - 160, 40, 200, 200), ("Sanity: " + sanity / (MAX_SANITY/100)), style1);	
	}
    */
    

    //test for picking up batteries and use them for Test Scene One
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("battery"))
        {
            if (inventory.addItem(other.gameObject))
            {
                other.gameObject.SetActive(false);
            }
        } else if (other.gameObject.CompareTag("note"))
        {
            Debug.Log(other.gameObject.GetComponent<NoteController>().GetContent());
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("zone_collider"))
        {
            if (other.gameObject.name == "ElevatorCollider")
            {
                Debug.Log("Player at Elevator");
                inElevator = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("zone_collider"))
        {
            if (other.gameObject.name == "ElevatorCollider")
            {
                inElevator = false;
            }
        }
    }

	public float getPower(){
		return internalBattery.getPowerLevel();
	}

	public void reducePower() {
        if (!internalBattery.takePower(1)) {
            endGame();
		}
	}

	public void gainSanity(){
		if (this.sanity < MAX_SANITY - 3) {
			this.sanity += 5;
		} else {
			this.sanity = MAX_SANITY;
		}

	}

	public void endGame(){
		SceneManager.LoadScene(0);
	}

    public Inventory GetInventory() {
        return this.inventory;
    }

    public int GetSanity() {
        return sanity / (MAX_SANITY / 100);
    }

    public PowerSource getPowerSource() {
        return internalBattery;
    }

}
