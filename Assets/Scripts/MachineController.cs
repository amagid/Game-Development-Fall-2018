using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

	[SerializeField] private Light light;
	[SerializeField] private GameObject player;
	private Inventory inventory;
	public static bool activated = false;
	MachinePartPosition[] allMachinePartPositions;

	// Use this for initialization
	void Start () {
		allMachinePartPositions = FindObjectsOfType<MachinePartPosition>();
	}

	public IEnumerator activate()
	{
		for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
		{
			if(light.range < 30f)
			{
				light.intensity += 0.8f;
				light.range += 0.2f;
			}
			else
			{
				break;
			}
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(1.0f);

		//add an indicator that the machine has been built
		player.GetComponent<PlayerCharacter>().pauseSanityLoss();

		for (float t = 0f; t < 1; t += Time.deltaTime / 2f)
		{
			if (light.range > 8f)
			{
				light.intensity -= 0.5f;
				light.range -= 0.2f;
			}
			else
			{
				break;
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

	// checks if every machine part is placed in the machine, if it is it activates the machine
	public void isMachineComplete(){
		for (int i = 0; i < allMachinePartPositions.Length; i++) {
			if (!allMachinePartPositions [i].getIfPartPlacedInMachine ()) {
				return;
			}
		}
		StartCoroutine ("activate");
		activated = true;
	}
}