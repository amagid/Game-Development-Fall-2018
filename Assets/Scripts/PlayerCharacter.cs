using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCharacter : MonoBehaviour {
	private GUIStyle style1 = new GUIStyle();
    private Inventory inventory;
	private const int MAX_POWER = 300;
	private int power;
	private const int MAX_SANITY = 1000;
	private int sanity;
	private bool losingPower; 
	private bool losingSanity;
    private bool inElevator;

    void Start () {
		style1.fontSize = 25;
		style1.normal.textColor = Color.green;
        inventory = GetComponent<Inventory>();
		power = MAX_POWER;
		sanity = MAX_SANITY;
		losingPower = false;
		losingSanity = true;
        inElevator = false; ;
    }
	
	// Update is called once per frame
	void Update () {
		if (inventory.itemCount () == 0 & losingSanity && !inElevator && false) {
			sanity--;
			if (sanity < 0) {
				endGame ();
			}
		}
	}

    /*
	void OnGUI() {
		GUI.Label (new Rect (Screen.width - 160, 0, 200, 200), ("Batteries: " + inventory.itemCount()), style1);
		GUI.Label (new Rect (Screen.width - 160, 20, 200, 200), ("Power: " + power / (MAX_POWER/100)), style1);
		GUI.Label (new Rect (Screen.width - 160, 40, 200, 200), ("Sanity: " + sanity / (MAX_SANITY/100)), style1);	
	}*/

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

	public int getPower(){
		return this.power;
	}

	public void reducePower(){
		this.power--;
		if(power < 0){
			endGame ();
		}
	}

	public void gainSanity(){
		if (this.sanity < MAX_SANITY - 3) {
			this.sanity += 5;
		} else {
			this.sanity = MAX_SANITY;
		}

	}


	public void stopReducingPower(){
		losingPower = false;
	}

	public void endGame(){
		SceneManager.LoadScene (0);
	}

    public Inventory GetInventory() {
        return this.inventory;
    }

    public int GetSanity() {
        return sanity / (MAX_SANITY / 100);
    }

    public int GetPower() {
        return power / (MAX_POWER / 100);
    }

}
