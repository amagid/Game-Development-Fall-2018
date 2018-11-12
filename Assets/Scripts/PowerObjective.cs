using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObjective : MonoBehaviour {

    [SerializeField] private string objectiveID;
    [SerializeField] private bool useBattery = true;
    [SerializeField] private bool usePersonalPower = true;
    private bool completed = false;
    private Operator op;

    private void Start()
    {
        this.op = GameObject.Find("Operator").GetComponent<Operator>();
    }

    public bool isComplete()
    {
        return this.completed;
    }

    public bool complete()
    {
        if (this.completed)
        {
            return true;
        }
        else
        {
            return this.completed = this.op.attemptCompleteMessage(this.objectiveID);
        }
    }

    public bool canUseBattery()
    {
        return this.useBattery;
    }

    public bool canUsePersonalPower()
    {
        return this.usePersonalPower;
    }
}
