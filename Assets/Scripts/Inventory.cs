using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {
    
    private GameObject[] itemList;

    // Only for initialization
    public int inventorySize;

    private int selectedItemIndex;
	// Use this for initialization
	void Start () {
        itemList = new GameObject[this.inventorySize];
	}

    //return the number of items the player currently have
    public int itemCount()
    {
        int count = 0;
        for (int i = 0; i < this.itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Add an item to the first available inventory slot. Returns true if successful, false if not.
    /// </summary>
    public bool addItem(GameObject item) {
        for (int i = 0; i < this.itemList.Length; i++)
        {
            if (this.itemList[i] == null)
            {
                this.itemList[i] = item;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Remove an item from the inventory. Returns true if successful, false if not.
    /// </summary>
	public bool removeItem(GameObject item) {
        for (int i = 0; i < this.itemList.Length; i++)
        {
            if (this.itemList[i] == item)
            {
                this.itemList[i] = null;
                return true;
            }
        }
        return false;
	}

    public bool hasItem() {
        return this.itemList.Length > 0;
    }

//    public bool hasBattery() {
  //      return batteryList.Count > 0;
    //}

    //check if the itemList contains a specific item
	public bool containItem(string itemName) {
        if(!hasItem()) {
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
        return null;
    }

    public GameObject getItem(int index)
    {
        return this.itemList[index];
    }

    public int getSelectedItemIndex()
    {
        return this.selectedItemIndex;
    }

    public void setSelectedItemIndex(int index)
    {
        this.selectedItemIndex = index;
    }

    public GameObject getSelectedItem()
    {
        GameObject gameObj = (GameObject)(itemList[selectedItemIndex]);
        this.removeItem(gameObj);
        return gameObj;
    }

    //retrieve the first battery in the inventory
    public GameObject getFirstBattery()
    {
        GameObject battery = null;
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].CompareTag("battery"))
            {
                battery = itemList[i];
                itemList[i] = null;
                return battery;
            }
        }
        return null;
    }

    //clear out all items
    public void clearItems() {
        for (int i = 0; i < this.itemList.Length; i++)
        {
            this.itemList[i] = null;
        }
    }
}
