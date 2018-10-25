using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

	private List<GameObject> batteryList;

    private List<GameObject> itemList;

	// Use this for initialization
	void Start () {
		batteryList = new List<GameObject> ();
        itemList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //return the number of batteries the player currently have
    public int batteryCount()
    {
        return batteryList.Count;
    }

    //add an item to the battery list or item List
    public void addItem(GameObject item) {
        //Debug.Log ("Item added: " + item.name);
        if (item.CompareTag("battery"))
        {
            batteryList.Add(item);
        }
        else
        {
            itemList.Add(item);
        }
	}

    //remove a specific item from the itemList
	public void removeItem(GameObject item) {
        //Debug.Log("Item removed: " + item.name);
        itemList.Remove (item);
	}

    //check if the itemList contains a specific item
	public bool containItem(string itemName) {
		foreach (GameObject obj in itemList) {
			if (obj.name == itemName) {
				return true;
			}
		}
		return false;
	}

    //check if the batteryList contains battery
    public bool containBattery()
    {
        foreach (GameObject obj in batteryList)
        {
            if (obj.CompareTag("battery"))
            {
                return true;
            }
        }
        return false; 
    }

    //retrieve the item in the itemList by name
    public GameObject getItem(string itemName)
    {
        foreach (GameObject obj in itemList)
        {
            if (obj.name == itemName)
            {
                return obj;
            }
        }
        throw new Exception("Item not found!");
    }

    //retrieve the first battery in the batteryList
    public GameObject getFirstBattery()
    {
		sortDescending ();
        GameObject gameObj = (GameObject)(batteryList[0]);
        batteryList.Remove(gameObj);
        return gameObj;
    }

    //sort the batterylist by descending power index
	public void sortDescending(){
		batteryList.Sort ((x, y) => (int)y.GetComponent<Battery> ().getPowerSource ().getPowerLevel ());
			//.getPowerIndex().CompareTo(x.GetComponent<Battery>().getPowerIndex()));
	}

    //clear out all battery and items
    public void clearItems() {
        while (batteryList.Count > 0)
        {
            batteryList.RemoveAt(0);
        }
        while (itemList.Count > 0)
        {
            itemList.RemoveAt(0);
        }
    }

    //clear out all batteries
    public void clearBatteries() {

    }
}
