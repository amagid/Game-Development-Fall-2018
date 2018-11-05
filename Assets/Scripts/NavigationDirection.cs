using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Determines the direction the player is traveling in
public class NavigationDirection : MonoBehaviour {
	// stores the player object
	[SerializeField] GameObject player;
	// stores the text to display the direction to
	[SerializeField] Text directionText;
	
	private enum CardinalDirection
	{
		North, South, East, West
	}

	// Update is called once per frame
	void Update () {
		var v = player.transform.forward;
		v.y = 0;
		v.Normalize ();
		if (Vector3.Angle (v, Vector3.forward) <= 45.0) {
			directionText.text = CardinalDirection.North.ToString();
		} else if (Vector3.Angle (v, Vector3.right) <= 45.0) {
			directionText.text = CardinalDirection.East.ToString();
		} else if (Vector3.Angle (v, Vector3.back) <= 45.0) {
			directionText.text = CardinalDirection.South.ToString();
		} else {
			directionText.text = CardinalDirection.West.ToString();
		}
	}
}
