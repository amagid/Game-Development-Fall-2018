using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDarknessController : MonoBehaviour
{
    public float onProbability = 0.9f;
    public float flickerDelay = 0.1f;
    [SerializeField] private GameObject darkness;

    private void Start()
    {
        this.flicker();
    }
    // Update is called once per frame
    private void flicker()
    {
        float val = UnityEngine.Random.value;
        if (val < onProbability)
        {
            this.darkness.SetActive(true);
        }
        else
        {
            this.darkness.SetActive(false);
        }
        Invoke("flicker", flickerDelay);
    }
}
