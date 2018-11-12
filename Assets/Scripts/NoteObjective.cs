using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjective : MonoBehaviour, Objective {

    [SerializeField] private string objectiveID;
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
}
