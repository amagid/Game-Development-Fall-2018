using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    public int max_power = 100;

    public int power_index = 100;

    public bool inUse;

	// Use this for initialization
	void Start () {
        inUse = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public bool isEmpty()
    {
        return power_index <= 0;
    }

    public int getPowerIndex() {
        return this.power_index;
    }

    public IEnumerator useBattery() {
        inUse = true;
        while (!isEmpty())
        {
            //battery will be empty in 5 seconds
            power_index -= 5;
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log(power_index);
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.red;
        this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.red;
    }

    public IEnumerator chargeBattery()
    {
        inUse = false;
        while(power_index <= max_power)
        {
            yield return new WaitForSeconds(1f);
            power_index = max_power;
        }
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        this.gameObject.transform.Find("light1").GetComponent<Light>().color = Color.green;
        this.gameObject.transform.Find("light2").GetComponent<Light>().color = Color.green;
    }


}
