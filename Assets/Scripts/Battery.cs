﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    private int power_index;

    private bool isPickedUp;

    private bool isEmpty;

    private bool inUse;

	// Use this for initialization
	void Start () {
        power_index = 100;
        isPickedUp = false;
        isEmpty = false;
        inUse = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (power_index <= 0) {
            isEmpty = true;
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
	}

    public int getPowerIndex() {
        return this.power_index;
    }

    public IEnumerator useBattery() {
        inUse = true;
        while (!isEmpty && isPickedUp) {
            power_index--;
            yield return new WaitForSeconds(1f);
        }
    }


}