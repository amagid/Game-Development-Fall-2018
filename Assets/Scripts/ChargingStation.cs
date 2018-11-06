using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{

	private PowerSource chargingStationPowerSource = new PowerSource(Mathf.Infinity, Mathf.Infinity);
	private Inventory inventory;
	private bool atSwitch = false;
	public double xOffset;
	public double yOffset;
	public double zOffset;

	void Start()
	{
		inventory = GameObject.Find("Player").GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update()
	{
		if (atSwitch)
		{
			if (Input.GetKeyDown(KeyCode.E) && inventory.itemCount() >= 3)
			{
				chargeBattery();
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.name == "Player")
			atSwitch = true;
	}

	void OnTriggerExit(Collider other)
	{
		atSwitch = false;
	}

	void chargeBattery()
	{
		for (int i = 0; i < 3; i++)
		{
			GameObject battery = inventory.getFirstBattery();
			battery.transform.position = transform.position - new Vector3(1f, 0f, 0f) + new Vector3((float)(i * xOffset), 0f, (float)(zOffset));
			battery.SetActive(true);
			if (battery.GetComponent<Battery>().getPowerSource().isEmpty())
			{
				this.StartCoroutine(chargeBattery(battery.GetComponent<Battery>()));
			}
		}
	}

	public IEnumerator chargeBattery(Battery battery)
	{

		while(!battery.getPowerSource().isFull())
		{
			yield return new WaitForSeconds(0.01f);
			battery.getPowerSource().givePower(1f);
		}
	}
}