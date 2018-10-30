using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Component class defines the initial specs of a battery contained in
/// a powered device. If a device has this component, then it starts with an
/// internal battery. This component determines the stats of that battery and
/// whether or not the player can take the battery out.
/// </summary>

public class InitialPowerSource : MonoBehaviour {

    [SerializeField] private bool extractable;
    [SerializeField] private float capacity;
    [SerializeField] private float initialPower;

	// Use this for initialization
	void Start () {
        PowerConsumer pc = this.gameObject.GetComponent<PowerConsumer>();
        PowerSource ps = new PowerSource(this.capacity, this.initialPower);
        pc.attachPowerSource(ps);
        pc.setPowerSourceExtractable(this.extractable);
	}
}