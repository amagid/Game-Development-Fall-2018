using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    public enum Type
    {
        Power
    }
    public Type type;
    public GameObject device;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.type == Type.Power)
            {
                PowerSource ps = this.device.GetComponent<PowerConsumer>().getPowerSource();
                if (ps != null) {
                    ps.takePower(ps.getPowerLevel());
                }
            }
        }
    }
}
