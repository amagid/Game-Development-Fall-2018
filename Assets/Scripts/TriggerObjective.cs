using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjective : MonoBehaviour, Objective {

    [SerializeField] private string objectiveID;
    [SerializeField] private bool destroyWhenCompleted = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && this.complete() && this.destroyWhenCompleted) {
            this.gameObject.SetActive(false);
        }
    }
}
