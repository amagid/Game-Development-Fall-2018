using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePartPosition : MonoBehaviour {

	private Renderer partRenderer;
	private bool partPlacedInMachine;
	MachineController theMachine;
	[SerializeField] private int partNumber;

	void Start() {
		theMachine = FindObjectOfType<MachineController> ();
		partPlacedInMachine = false;
		partRenderer = GetComponent<Renderer> ();
		partRenderer.enabled = true;
	}


	public int getPartNumber(){
		return this.partNumber;
	}

	// changes the material of the missing machine part from the transparent material to the actual part texture
	// checks to see if this was the last machine placed in the machine and if it was it activates the machine
	public void placePartInMachine(Material partsNewMaterial){
		partRenderer.sharedMaterial = partsNewMaterial;
		this.partPlacedInMachine = true;
		theMachine.isMachineComplete ();
	}

	public bool getIfPartPlacedInMachine(){
		return this.partPlacedInMachine;
	}
}