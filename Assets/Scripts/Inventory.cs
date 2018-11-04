using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

	private List<GameObject> batteryList;

    private List<GameObject> itemList;

    public int selectedBatteryIndex;

	// Use this for initialization
	void Start () {
		batteryList = new List<GameObject> ();
        itemList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //return the number of items the player currently have
    public int itemCount()
    {
        return itemList.Count;
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

    public bool hasItem() {
        return itemList.Count > 0;
    }

    public bool hasBattery() {
        return batteryList.Count > 0;
    }

    //check if the itemList contains a specific item
	public bool containItem(string itemName) {
        if(! (hasItem())) {
            return false;
        }
		foreach (GameObject obj in itemList) {
			if (obj.name == itemName) {
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

    public GameObject getSelectedBattery()
    {
        if (selectedBatteryIndex == -1)
        {
            return null;
        }
        GameObject gameObj = (GameObject)(batteryList[selectedBatteryIndex]);
        batteryList.Remove(gameObj);
        return gameObj;
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
		batteryList.Sort ((x, y) => (int)y.GetComponent<Battery> ().getPowerSource ().getPowerLevel ().CompareTo((int)x.GetComponent<Battery> ().getPowerSource ().getPowerLevel ()));
			//.getPowerIndex().CompareTo(x.GetComponent<Battery>().getPowerIndex()));
	}

    //clear out all items
    public void clearItems() {
        while (itemList.Count > 0)
        {
            itemList.RemoveAt(0);
        }
    }

    //clear out all batteries
    public void clearBatteries() {
        while (batteryList.Count > 0)
        {
            batteryList.RemoveAt(0);
        }
    }
}
