using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralLightController : MonoBehaviour, DirectOperation {

	private Inventory inventory;
	[SerializeField] private GameObject central_lights;

	private bool deviceActive = false;
	private GameObject battery;

	void Start()
	{
		this.deviceActive = false;
		inventory = GameObject.Find("Player").GetComponent<Inventory>();
	}


	public void activate()
	{
		central_lights.SetActive(true);
	}

	public void operate() {}

	public void deactivate()
	{

		this.battery = null;
		central_lights.SetActive(false);
		deviceActive = false;
	}

	public bool isActive()
	{
		return deviceActive;
	}
		

	public void setBattery(GameObject bat)
	{
		this.battery = bat;
	}
}
