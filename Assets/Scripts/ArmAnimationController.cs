using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J))
        {
            this.playAnimation();
        }
	}

    public void playAnimation()
    {
        this.GetComponent<Animation>().Play();
    }


}
