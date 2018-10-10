using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPowerRegeneration : MonoBehaviour {

    [SerializeField] private float autoRegenRate;
    private PowerConsumer powerConsumer;

    void Start()
    {
        this.powerConsumer = this.GetComponent<PowerConsumer>();
    }

    // Update is called once per frame
    void Update () {
        this.powerConsumer.getPowerSource().givePower(this.autoRegenRate);
	}
}
