using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Determines the direction the player is traveling in
public class NavigationDirection : MonoBehaviour {
	// stores the player object
	[SerializeField] GameObject player;
	[SerializeField] Image compass;
	[SerializeField] Sprite north;
	[SerializeField] Sprite south;
	[SerializeField] Sprite east;
	[SerializeField] Sprite west;


	void Start(){
		compass.sprite = east;
	}

	// Update is called once per frame
	void Update () {
		var v = player.transform.forward;
		v.y = 0;
		v.Normalize ();
		if (Vector3.Angle (v, Vector3.forward) <= 45.0) {
			compass.sprite = north;
		} else if (Vector3.Angle (v, Vector3.right) <= 45.0) {
			compass.sprite = east;
		} else if (Vector3.Angle (v, Vector3.back) <= 45.0) {
			compass.sprite = south;
		} else {
			compass.sprite = west;
		}
	}
}
