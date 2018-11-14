using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour {

    public float onProbability = 0.9f;
    public float flickerDelay = 0.1f;
    public GameObject light;

    private void Start()
    {
        this.flicker();
    }
    // Update is called once per frame
    private void flicker() {
        float val = UnityEngine.Random.value;
		if (val < onProbability)
        {
            this.light.SetActive(true);
        } else
        {
            this.light.SetActive(false);
        }
        Invoke("flicker", flickerDelay);
	}
}
