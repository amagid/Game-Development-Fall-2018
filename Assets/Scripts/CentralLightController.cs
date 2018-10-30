using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralLightController : MonoBehaviour, DirectOperation {

	private Inventory inventory;
	[SerializeField] private GameObject central_lights;
	private GameObject GreenLights;
	private GameObject RedLights;

	private bool deviceActive = false;
	private GameObject battery;

	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		this.deviceActive = false;
		inventory = GameObject.Find("Player").GetComponent<Inventory>();
		Transform gl = this.gameObject.transform.Find("GreenLights");
		Transform rl = this.gameObject.transform.Find("RedLights");
		if (gl != null)
		{
			this.GreenLights = gl.gameObject;
		}
		if (rl != null)
		{
			this.RedLights = rl.gameObject;
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



	// Update is called once per frame
	void Update()
	{
	}

	public void activate()
	{
		//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		if (this.GreenLights != null && !this.GreenLights.activeInHierarchy)
		{
			this.GreenLights.SetActive(true);
		}
		if (this.RedLights != null && this.RedLights.activeInHierarchy)
		{
			this.RedLights.SetActive(false);
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


		central_lights.SetActive(true);
	}

	public void operate() {}

	public void deactivate()
	{
		//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		if (this.GreenLights != null && this.GreenLights.activeInHierarchy)
		{
			this.GreenLights.SetActive(false);
		}
		if (this.RedLights != null && !this.RedLights.activeInHierarchy)
		{
			this.RedLights.SetActive(true);
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
