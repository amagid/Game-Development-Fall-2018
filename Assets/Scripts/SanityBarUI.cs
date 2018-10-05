using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    Image sanityBar;
    float maxSanity = 100f;
    public static float sanity;

	// Use this for initialization
	void Start () {
        sanityBar = GetComponent<Image>();
        sanity = maxSanity;
	}
	
	// Update is called once per frame
	void Update () {
        sanityBar.fillAmount = sanity / maxSanity;
	}
}
