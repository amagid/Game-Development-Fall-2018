using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for indicator lights. A lot of devices will have some kind of indicator
/// that will turn on when they reach a certain Power level. This is how we do
/// that. All you have to do is add an entry for each indicator in the
/// PowerConsumer, set the "indicator" to the GameObject which contains the light
/// and set its minPower and maxPower thresholds to determine when it is shown.
/// </summary>
public class IndicatorLight : MonoBehaviour {

    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;
    [SerializeField] private GameObject indicator;
    private PowerConsumer powerConsumer;

    // Use this for initialization
    void Start () {
        this.powerConsumer = this.GetComponent<PowerConsumer>();
	}
	
	// Update is called once per frame
	void Update () {
        float power = this.powerConsumer.getPowerSource().getPowerLevel();
        if (power >= minPower && power <= maxPower)
        {
            this.indicator.SetActive(true);
        } else
        {
            this.indicator.SetActive(false);
        }
	}
}
