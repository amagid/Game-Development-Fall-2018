using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	private List<GameObject> items;

	// Use this for initialization
	void Start () {
		items = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public int itemCount()
    {
        return items.Count;
    }

    //****************************************
    //need to sort the batteries from largest power_index to lowest!!!!!!!!!!!!!!!!!!!!!!

    public bool addItem(GameObject item) {
		Debug.Log ("Item added: " + item.name);
        items.Add(item);
        return true;
	}

	public bool removeItem(GameObject item) {
        Debug.Log("Item removed: " + item.name);
        items.Remove (item);
        return false;
	}

	public bool containItem(string itemName) {
		foreach (Object obj in items) {
			if (obj.name == itemName) {
				return true;
			}
		}
		return false;
	}
		
	public void dropItem() {
		GameObject gameObj = (GameObject) (items [0]);
        gameObj.transform.position = transform.position + new Vector3(0, 0, 5);
		gameObj.SetActive (true);
		removeItem (gameObj);
	}

    //test method for test scene 1 ONLY
    public GameObject getFirstItem()
    {
		sortDescending ();
        GameObject gameObj = (GameObject)(items[0]);
        removeItem(gameObj);
        return gameObj;
    }

	public void sortDescending(){
		items.Sort ((x, y) => y.GetComponent<Battery>().getPowerIndex().CompareTo(x.GetComponent<Battery>().getPowerIndex()));
	}
}
