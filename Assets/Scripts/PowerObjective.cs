using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObjective : MonoBehaviour {

    [SerializeField] private string objectiveID;
    [SerializeField] private bool useBattery = true;
    [SerializeField] private bool usePersonalPower = true;
    private bool completed = false;
    private Operator op;
    private PowerConsumer pc;
    private PowerConsumer playerPC;

    private void Start()
    {
        this.op = GameObject.Find("Operator").GetComponent<Operator>();
        this.pc = this.GetComponent<PowerConsumer>();
        this.playerPC = GameObject.Find("Player").GetComponent<PowerConsumer>();
    }

    private void Update()
    {
        if (this.pc.canPowerDevice())
        {
            if (this.useBattery && this.pc.getPowerSource() != this.playerPC.getPowerSource())
            {
                this.complete();
            } else if (this.usePersonalPower && this.pc.getPowerSource() == this.playerPC.getPowerSource())
            {
                this.complete();
            }
        }
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
            this.completed = this.op.attemptCompleteMessage(this.objectiveID);
            if (this.completed)
            {
                this.enabled = false;
            }
            return this.completed;
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
