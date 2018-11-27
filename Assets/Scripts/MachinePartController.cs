using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePartController : MonoBehaviour {
	public enum machinePartName {MachinePart1,MachinePart2};

	[SerializeField] private int partNumber;

	// deletes the object and adds it to the inventory
	public void pickUpObject(Inventory inventory){
		inventory.addItem(gameObject); 
		gameObject.SetActive (false);
	}

	public int getPartNumber(){
		return this.partNumber;
	}
}