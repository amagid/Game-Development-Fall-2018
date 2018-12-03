using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningChamberController : MonoBehaviour, DirectOperation {

    private int numSwitches = 0;
    private bool deviceActive = false;
    private Vector3 finalPosition;

    private void Start()
    {
        this.finalPosition = new Vector3(this.transform.position.x, -6f, this.transform.position.z);
    }

    private void Update()
    {
        if (this.isActive())
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.finalPosition, Time.deltaTime / 3);
            Invoke("setInactive", 10f);
        }
    }

    public void activate()
    {
        this.numSwitches++;
        if (this.numSwitches >= 2)
        {
            this.deviceActive = true;
            this.GetComponent<AudioSource>().Play();
        }
    }

    public void operate()
    {

    }

    public void deactivate()
    {
        this.numSwitches--;
    }

    public bool isActive()
    {
        return this.deviceActive;
    }

    private void setInactive()
    {
        this.gameObject.SetActive(false);
    }
}
